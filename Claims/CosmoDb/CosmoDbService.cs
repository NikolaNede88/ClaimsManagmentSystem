using Claims.CosmoDb;
using Claims.Models;
using Microsoft.Azure.Cosmos;


public class CosmoDbService : ICosmoDbService
{
    #region Properties

    private readonly Container _container;

    #endregion

    #region Constructor

    public CosmoDbService(CosmosClient dbClient,
       string databaseName,
       string containerName)
    {
        if (dbClient == null) throw new ArgumentNullException(nameof(dbClient));
        _container = dbClient.GetContainer(databaseName, containerName);

    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        var query = _container.GetItemQueryIterator<Claim>(new QueryDefinition("SELECT * FROM c"));

        var results = new List<Claim>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();

            results.AddRange(response.ToList());
        }
        return results;

    }

    public async Task<Claim> GetClaimAsync(string id)
    {
        var response = await _container.ReadItemAsync<Claim>(id, new PartitionKey(id));

        return response.Resource;
    }

    public Task AddItemAsync(Claim item)
    {
        var response = _container.CreateItemAsync(item, new PartitionKey(item.Id));

        return response;
    }

    public Task DeleteItemAsync(string id)
    {
        var response = _container.DeleteItemAsync<Claim>(id, new PartitionKey(id));

        return response;
    }
}
#endregion