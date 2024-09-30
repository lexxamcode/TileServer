using Elastic.Clients.Elasticsearch;
using Gdd.Domain.Model;
using Gdd.Domain.Model.Requests;
using Gdd.Domain.Services;
namespace TileProxyServer.Services;

/// <summary>
/// Репозиторий для работы с ElasticSearch
/// </summary>
/// <param name="elasticsearchClient">Клиент ElasticSearch</param>
public class RequestsRepository(ElasticsearchClient elasticsearchClient) : IRequestsRepository
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

    /// <summary>
    /// Добавление запроса в индекс ElasticSearch
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <returns>Идентификатор добавленного запроса</returns>
    public async Task<string> AddRequest(Request request)
    {
        var response = await _elasticsearchClient.IndexAsync(request);

        return response.Id;
    }

    public async Task<IEnumerable<Request>> GetAllRequestsByIp(string clientIp, GetListRequestFilter? filter)
    {
        filter ??= new GetListRequestFilter();

        var queryResponse = await _elasticsearchClient.SearchAsync<Request>(s => s
                                                                                .From(filter.From)
                                                                                .Size(filter.Size)
                                                                                .Sort(sr => sr.Field(f => f.RequestTime))
                                                                                .Query(q => q
                                                                                            .Match(m => m
                                                                                            .Field(f => f.ClientIp)
                                                                                            .Query(clientIp))));

        return queryResponse.Documents;
    }
}
