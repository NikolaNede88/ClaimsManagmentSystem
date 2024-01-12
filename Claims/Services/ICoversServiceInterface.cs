using Claims.Models;


namespace Claims.Services
{
    public interface ICoversServiceInterface
    {
        #region Methods

        Task<IEnumerable<Cover>> GetAsync();

        Task<Cover> GetAsync(string id);

        Task<Cover> CreateAsync(Cover cover);

        Task DeleteAsync(string id);     

        #endregion
    }
}
