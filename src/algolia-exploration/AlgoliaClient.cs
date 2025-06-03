using Algolia.Search.Clients;
using Algolia.Search.Iterators;
using Algolia.Search.Models.Common;

namespace algolia_exploration;

public class AlgoliaClient(string indexName, string appId, string apiKey)
{
    private readonly ISearchClient _searchClient = new SearchClient(appId, apiKey);

    public async Task<List<T>> GetAllRecordsAsync<T>(BrowseIndexQuery query)
        where T : class
    {
        var index = GetIndex(indexName);
        var allRecords = new List<T>();
        BrowseIndexResponse<T> response;
        
        do
        {
            response = await index.BrowseFromAsync<T>(query);
            if (response?.Hits != null)
            {
                allRecords.AddRange(response.Hits);
            }

            query.Cursor = response?.Cursor;
        } while (!string.IsNullOrEmpty(response?.Cursor));

        return allRecords;
    }

    private ISearchIndex GetIndex(string indexName)
    {
        return _searchClient.InitIndex(indexName);
    }
}