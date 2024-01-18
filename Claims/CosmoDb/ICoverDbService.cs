using Claims.Models;

namespace Claims.CosmoDb
{
    public interface ICoverDbService
    {
        #region Methods

        Task<IEnumerable<Cover>> GetAsync();

        Task<Cover> GetAsync(string id);

        Task<Cover> CreateAsync(Cover cover);

        Task DeleteAsync(string id);

        #endregion
    }
}
