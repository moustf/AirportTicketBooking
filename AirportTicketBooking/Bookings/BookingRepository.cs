using System.Linq;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Passengers;
using CsvHelper;

namespace AirportTicketBooking.Bookings
{
    public class BookingRepository
    {
        private readonly CSVIOService _csvioService;
        private readonly CsvReader _bookingCsvReader;
        private readonly CsvReader _flightCsvReader;
        private readonly CsvReader _passengerCsvReader;

        public BookingRepository(CSVIOService csvioService, CsvReader bookingCsvReader, CsvReader flightCsvReader, CsvReader passengerCsvReader)
        {
            _csvioService = csvioService;
            _bookingCsvReader = bookingCsvReader;
            _flightCsvReader = flightCsvReader;
            _passengerCsvReader = passengerCsvReader;
        }
        
        public Booking[] SearchForBookingsBy(string prop, string value)
        {
            var bookings = _csvioService.GetAllRecords<Booking>("Booking", _bookingCsvReader);
            var flightIdsFirst = bookings.Select(booking => booking.FlightId).ToArray();
            var allFlights = _csvioService.GetAllRecords<Flight>("Flight", _flightCsvReader);
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
            var bookings = _csvioService.GetAllRecords<Booking>("Booking", _bookingCsvReader);
            var passengerIds = _csvioService.GetAllRecords<Passenger>("Passenger", _passengerCsvReader)
                .Where(passenger => passenger.PassengerId == passengerId)
                .Select(passenger => passenger.PassengerId).ToArray();

            return bookings.Where(booking => passengerIds.Contains(booking.PassengerId)).ToArray();
        }

        public Booking[] GetBookings()
        {
            return  _csvioService.GetAllRecords<Booking>("Booking", _bookingCsvReader);
        }
    }
}