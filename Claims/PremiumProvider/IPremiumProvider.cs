using Claims.Models;

namespace Claims.PremiumProvider
{
    public interface IPremiumProvider
    {
        #region Methods

        decimal ComputePremium(int insuranceLength, decimal premiumPerDay);

        #endregion
    }
}
