namespace HotelReservationLibrary
{
    public interface IReservationValidator
    {
        bool IsValid(Reservation reservation);
    }

    public class ReservationValidator : IReservationValidator
    {
        public bool IsValid(Reservation reservation)
        {
            ArgumentNullException.ThrowIfNull(reservation);

            if (string.IsNullOrEmpty(reservation.GuestFirstName)) return false;

            if (string.IsNullOrEmpty(reservation.GuestLastName)) return false;

            if (!reservation.guestEmail.Contains('@')) return false;

            if (reservation.CheckOutDate <= reservation.CheckInDate) return false;

            if (reservation.NumberOfAdditionalGuests > 2) return false;

            if (string.IsNullOrEmpty(reservation.RoomType)) return false;

            return true;
        }
    }
}