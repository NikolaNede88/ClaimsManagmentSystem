using Claims.Auditing;
using Claims.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Claims.Services
{
    public class CoversService : ICoversServiceInterface
    {

        #region Properties

        private readonly Container _container;

        private readonly Auditer _auditer;

        #endregion

        #region Constructor

        public CoversService(CosmosClient cosmosClient, AuditContext auditContext)
        {
            _container = cosmosClient?.GetContainer("ClaimDb", "Cover")
                    ?? throw new ArgumentNullException(nameof(cosmosClient));

            _auditer = new Auditer(auditContext) ?? throw new ArgumentNullException(nameof(auditContext));
        }

        public decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        public async Task<Cover> CreateAsync(Cover cover)
        {
            cover.Id = Guid.NewGuid().ToString();
            cover.Premium = ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
            await _container.CreateItemAsync(cover, new PartitionKey(cover.Id));
            _auditer.AuditCover(cover.Id, "POST");
            return cover;
        }

        public async Task DeleteAsync(string id)
        {
            _auditer.AuditCover(id, "DELETE");
            await _container.DeleteItemAsync<Cover>(id, new(id));
        }

        public async Task<IEnumerable<Cover>> GetAsync()
        {
            var query = _container.GetItemQueryIterator<Cover>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<Cover>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results.ToList();
        }

        public async Task<Cover> GetAsync(string id)
        {
            var result = await _container.ReadItemAsync<Cover>(id, new(id));

            return result;
        }

        #endregion
    }
}
