using System.Linq;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Passengers;
using CsvHelper;

namespace AirportTicketBooking.Bookings
{
    public class BookingRepository
    {
        private readonly CSVIOService _csvioService;
        private readonly CsvReader _csvReader;

        public BookingRepository(CSVIOService csvioService, CsvReader csvReader)
        {
            _csvioService = csvioService;
            _csvReader = csvReader;
        }
        
        public Booking[] SearchForBookingsBy(string prop, string value)
        {
            var bookings = _csvioService.GetAllRecords<Booking>("Booking", _csvReader);
            var flightIdsFirst = bookings.Select(booking => booking.FlightId).ToArray();
            var allFlights = _csvioService.GetAllRecords<Flight>("Flight", _csvReader);
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
            var bookings = _csvioService.GetAllRecords<Booking>("Booking", _csvReader);
            var passengerIds = _csvioService.GetAllRecords<Passenger>("Passenger", _csvReader)
                .Where(passenger => passenger.PassengerId == passengerId)
                .Select(passenger => passenger.PassengerId).ToArray();

            return bookings.Where(booking => passengerIds.Contains(booking.PassengerId)).ToArray();
        }

        public Booking[] GetBookings()
        {
            return  _csvioService.GetAllRecords<Booking>("Booking", _csvReader);
        }
    }
}