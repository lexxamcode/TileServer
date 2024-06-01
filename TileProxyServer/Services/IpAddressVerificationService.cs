using Domain.Model;
using Elastic.Clients.Elasticsearch;

namespace TileProxyServer.Services;

/// <summary>
/// Сервис для проверки осуществления массового скачивания
/// </summary>
/// <param name="elasticsearchClient">Клиент ElasticSearch</param>
public class IpAddressVerificationService(ElasticsearchClient elasticsearchClient) : IIpAddressVerificationService
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

    /// <summary>
    /// Метод валидации текущего запроса
    /// </summary>
    /// <param name="request">Объект текущего запроса</param>
    /// <returns>Является ли запрос потенциально опасным</returns>
    public async Task<bool> IsPotencialIntruderAsync(Request request)
    {
        var queryResponse = await _elasticsearchClient.SearchAsync<Request>(s => s
                                                            .Index("tile-server-index")
                                                            .From(0)
                                                            .Size(200)
                                                            .Query(q => q
                                                                         .Match(m => m
                                                                            .Field(f => f.ClientIp)
                                                                            .Query(request.ClientIp)
                                                                         )
                                                                    ));

        if (queryResponse.Documents.Count < 100)
            return false;

        var documentsSortedByCoordinates = queryResponse.Documents.OrderBy(request => request.Z).ThenBy(request => request.X).ThenBy(request => request.Y);
        var documentsSortedByTime = queryResponse.Documents.OrderBy(request => request.RequestTime).ToList();

        var areDocumentSequencesEqual = documentsSortedByCoordinates.SequenceEqual(documentsSortedByTime);

        return areDocumentSequencesEqual;
    }

    /// <summary>
    /// Добавление запроса в индекс ElasticSearch
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <returns></returns>
    public async Task<string> IndexRequestAsync(Request request)
    {
        var response = await _elasticsearchClient.IndexAsync(request);

        return response.Id;
    }
}
