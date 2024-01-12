
using Claims.Auditing;
using Claims.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Cosmos;
using System.Web.Http.ModelBinding;

namespace Claims.Services
{
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

        #endregion

        #region Methods

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
            try
            {
                var response = await _container.ReadItemAsync<Claim>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public Task AddItemAsync(Claim item)
        {
            return _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public Task DeleteItemAsync(string id)
        {
            return _container.DeleteItemAsync<Claim>(id, new PartitionKey(id));
        }

        #endregion
    }
}
