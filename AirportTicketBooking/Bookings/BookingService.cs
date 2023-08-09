using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Bookings
{
    public class BookingService
    {
        public Booking CreateNewBooking(BookingDto bookingData)
        {
            var booking = new Booking()
            {
                BookingId = bookingData.Id,
                DateOfBooking = bookingData.DateOfBooking,
                FlightId = bookingData.FlightId,
                PassengerId = bookingData.PassengerId,
                SeatsNumber = bookingData.SeatsNum,
            };

            return booking;
        }
    }
}