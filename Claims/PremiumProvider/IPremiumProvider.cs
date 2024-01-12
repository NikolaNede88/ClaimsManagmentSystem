using Claims.Models;

namespace Claims.PremiumProvider
{
    public interface IPremiumProvider
    {
        decimal ComputePremium(int insuranceLength, decimal premiumPerDay);
    }
}
