
using Claims.CosmoDb;
using Claims.Models;


namespace Claims.Services
{
    public class ClaimsService : ICmaimsService
    {
        #region Properties 

        private readonly ICosmoDbService _cosmoDbService;

        #endregion

        #region Constructor

        public ClaimsService(ICosmoDbService cosmoDbService)
        {
            _cosmoDbService = cosmoDbService ?? throw new ArgumentNullException(nameof(cosmoDbService)); ;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            var results = await _cosmoDbService.GetClaimsAsync();

            return (IEnumerable<Claim>)results;

        }

        public async Task<Claim> GetClaimAsync(string id)
        {
            var response = await _cosmoDbService.GetClaimAsync(id);

            return response;
        }

        public Task AddItemAsync(Claim item)
        {
            var response = _cosmoDbService.AddItemAsync(item);

            return response;

        }

        public Task DeleteItemAsync(string id)
        {
            return _cosmoDbService.DeleteItemAsync(id);

        }

        #endregion
    }
}
