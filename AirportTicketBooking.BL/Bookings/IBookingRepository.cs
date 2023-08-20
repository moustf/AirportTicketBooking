namespace AirportTicketBooking.Bookings
{
    public interface IBookingRepository
    {
        Booking[] SearchForBookingsBy(string property, string value);
        Booking[] SearchForBookingByPassenger(int passengerId);
        Booking[] GetBookings();
    }
}