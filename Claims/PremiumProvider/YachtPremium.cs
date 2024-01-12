namespace Claims.PremiumProvider
{
    public class YachtPremium : IPremiumProvider
    {
        #region Methods

        public decimal ComputePremium(int insuranceLength, decimal premiumPerDay)
            {
                decimal totalPremium = 0m;

                for (var i = 0; i < insuranceLength; i++)
                {
                    if (i < 30) totalPremium += premiumPerDay;
                    if (i < 180) totalPremium += premiumPerDay - premiumPerDay * 0.05m;
                }

                return totalPremium;
            }

        #endregion
    }

}
