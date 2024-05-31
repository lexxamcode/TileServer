using Domain.Model;
using Elastic.Clients.Elasticsearch;

namespace TileProxyServer.Services;

public class IpAddressVerificationService(ElasticsearchClient elasticsearchClient) : IIpAddressVerificationService
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

    public async Task<bool> IsPotencialIntruderAsync(Request request)
    {
        var queryResponse = await _elasticsearchClient.SearchAsync<Request>(query => query
                                                                            .Index("tile-server-index")
                                                                            .Size(2));

        var documentsSortedByCoordinates = queryResponse.Documents.OrderBy(request => request.Z).ThenBy(request => request.X).ThenBy(request => request.Y);
        var documentsSortedByTime = queryResponse.Documents.OrderBy(request => request.RequestTime).ToList();

        var areDocumentSequencesEqual = documentsSortedByCoordinates.SequenceEqual(documentsSortedByTime);

        return areDocumentSequencesEqual;
    }

    public async Task<string> IndexRequestAsync(Request request)
    {
        var response = await _elasticsearchClient.IndexAsync(request);

        return response.Id;
    }
}
