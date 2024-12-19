namespace HotelReservationLibrary
{
    public interface IReservationRepository
    {
        long Add(Reservation reservation);
    }

    public class ReservationRepository : IReservationRepository
    {
        public long Add(Reservation reservation)
        {
            return ReservationDb.AddReservation(reservation);
        }
    }
}