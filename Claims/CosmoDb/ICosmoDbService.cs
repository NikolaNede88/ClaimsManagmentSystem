﻿
using Claims.Models;

namespace Claims.CosmoDb
{
    public interface ICosmoDbService
    {
        #region Methods

        Task<IEnumerable<Claim>> GetClaimsAsync();

        Task<Claim> GetClaimAsync(string id);

        Task AddItemAsync(Claim item);

        Task DeleteItemAsync(string id);

        #endregion

    }
}
