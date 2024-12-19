namespace HotelReservationLibrary
{
    public class ReservationService
    {

        private readonly IReservationValidator _validator;
        private readonly IRoomPricingStrategy _pricing;
        private readonly IReservationRepository _repository;
        private readonly IWeatherApi _weather;

        public ReservationService(
            IReservationValidator validator = null,
            IRoomPricingStrategy pricing = null,
            IReservationRepository repository = null,
            IWeatherApi weather = null)
        {
            _validator = validator ?? new ReservationValidator();
            _pricing = pricing ?? new PricingStrategy();
            _repository = repository ?? new ReservationRepository();
            _weather = weather ?? new ExternalWeatherApi();
        }


        public long BookReservation(Reservation reservation)
        {
            if (!_validator.IsValid(reservation)) return 0;

            reservation.PricePerNight = _pricing.GetBasePrice(
                reservation.RoomType
            );

            if (reservation.PricePerNight == 0) return 0;

            reservation.PricePerNight = _pricing.ApplySmokingSurcharge(
                reservation.PricePerNight,
                reservation.SmokingOrNonSmoking
            );

            string forecastSummary = string.Empty;
            try
            {
                forecastSummary = _weather.GetForecast(
                    DateOnly.FromDateTime(reservation.CheckInDate),
                    DateOnly.FromDateTime(reservation.CheckOutDate)
                )?.Summary ?? string.Empty;
            }
            catch
            {
                Console.WriteLine("Failed to get weather forecast.");
            }

            reservation.PricePerNight = _pricing.ApplyWeatherSurcharge(
                reservation.PricePerNight,
                forecastSummary
            );


            reservation.Total =
                reservation.PricePerNight * (
                    reservation.CheckOutDate - reservation.CheckInDate
                ).Days;

            return _repository.Add(reservation);
        }
    }
}