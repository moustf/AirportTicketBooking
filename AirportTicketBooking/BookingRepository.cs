using System.Linq;

namespace AirportTicketBooking
{
    public class BookingRepository
    {
        public Booking[] SearchForBookingsBy(string prop, string value)
        {
            var csvio = new CSVIO();
            
            var bookings = csvio.GetAllRecords<Booking>("Booking");
            var flightIdsFirst = bookings.Select(booking => booking.FlightId).ToArray();
            var allFlights = csvio.GetAllRecords<Flight>("Flight");
            var flightsToSearchIn = allFlights.Where(flight => flightIdsFirst.Contains(flight.FlightId)).ToArray();
            
            var flightIds = flightsToSearchIn.Where(
                flight => prop == "DepartureDate"
                ? flight.GetType().GetProperty(prop)?.GetValue(flight).ToString().Split('/')[0] == value
                : flight.GetType().GetProperty(prop)?.GetValue(flight).ToString() == value
                ).Select(flight => flight.FlightId);

            return bookings.Where(booking => flightIds.Contains(booking.FlightId)).ToArray();
        }
        
        public Booking[] SearchForBookingByPassenger(int passengerId)
        {
            var csvio = new CSVIO();
            
            var bookings = csvio.GetAllRecords<Booking>("Booking");
            var passengerIds = csvio.GetAllRecords<Passenger>("Passenger")
                .Where(passenger => passenger.PassengerId == passengerId)
                .Select(passenger => passenger.PassengerId).ToArray();

            return bookings.Where(booking => passengerIds.Contains(booking.PassengerId)).ToArray();
        }
    }
}