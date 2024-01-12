namespace Claims.PremiumProvider
{
    public class TankerPremium : IPremiumProvider
    {
        public decimal ComputePremium(int insuranceLength, decimal premiumPerDay)
        {
            decimal totalPremium = 0m;

            for (var i = 0; i < insuranceLength; i++)
            {
                if (i < 30) totalPremium += premiumPerDay;
                if (i < 365) totalPremium += premiumPerDay - premiumPerDay * 0.08m;
            }

            return totalPremium;
        }
    }
}
