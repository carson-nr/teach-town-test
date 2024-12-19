namespace HotelReservationLibrary
{
    public class Reservation
    {
        public string GuestFirstName { get; set; } = string.Empty;
        public string GuestLastName { get; set; } = string.Empty;
        public string guestEmail { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdditionalGuests { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public string SmokingOrNonSmoking { get; set; } = string.Empty;
        internal double PricePerNight { get; set; }
        internal double Total { get; set; }

    }
}