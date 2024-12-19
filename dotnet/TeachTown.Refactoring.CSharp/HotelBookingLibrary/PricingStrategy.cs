namespace HotelReservationLibrary
{
    public interface IRoomPricingStrategy
    {
        double GetBasePrice(string roomType);
        double ApplySmokingSurcharge(double basePrice, string smokingPreference);
        double ApplyWeatherSurcharge(double currentTotal, string forecastSummary);
    }

    public class PricingStrategy : IRoomPricingStrategy
    {
        public double GetBasePrice(string roomType)
        {
            double basePrice = 0;

            switch (roomType)
            {
                case "Single":
                    basePrice = PricingConstants.SingleRoomBasePrice;
                    break;
                case "Double":
                    basePrice = PricingConstants.DoubleRoomBasePrice;
                    break;
                case "Suite":
                    basePrice = PricingConstants.SuiteRoomBasePrice;
                    break;
            }

            return basePrice;
        }

        public double ApplySmokingSurcharge(double basePrice, string smokingPreference)
        {
            if (smokingPreference == "Smoking")
                return basePrice * PricingConstants.SmokingFeeMultiplier;

            return basePrice;
        }

        public double ApplyWeatherSurcharge(double basePrice, string forecastSummary)
        {
            if (forecastSummary == "Sweltering" || forecastSummary == "Freezing")
                return basePrice * PricingConstants.ExtremeWeatherMultiplier;

            return basePrice;
        }

    }
}