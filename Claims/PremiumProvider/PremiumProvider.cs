using Claims.Models;

namespace Claims.PremiumProvider
{
    public class PremiumProvider 
    {
        #region Methods

        public decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
        {
            var multiplier = GetMultiplierForCoverType(coverType);
            var premiumPerDay = 1250 * multiplier;
            var insuranceLength = endDate.DayNumber - startDate.DayNumber;
           
            var provider = GetPremiumProvider(coverType);


            return provider.ComputePremium(insuranceLength, premiumPerDay);
        }

        private decimal GetMultiplierForCoverType(CoverType coverType)
        {
            switch (coverType)
            {
                case CoverType.Yacht:
                    return 1.1m;
                case CoverType.PassengerShip:
                    return 1.2m;
                case CoverType.Tanker:
                    return 1.5m;
                default:
                    return 1.3m;
            }
        }
        private IPremiumProvider GetPremiumProvider(CoverType coverType)
        {          
         
            switch (coverType)
            {
                case CoverType.Yacht:
                    return new YachtPremium();
                case CoverType.PassengerShip:
                    return new PassengerShipPremium();
                case CoverType.Tanker:
                    return new TankerPremium();
                default:
                    throw new InvalidOperationException("Invalid cover type");
            }
        }

        #endregion
    }
}

