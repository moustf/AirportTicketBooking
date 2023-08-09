using System;
using System.Linq;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Passengers;
using CsvHelper;

namespace AirportTicketBooking.Bookings
{
    public class BookingRepository
    {
        private readonly CSVIOService _csvioService;
        private readonly CSVReaderService _bookingCsvService;
        private readonly CSVReaderService _flightCsvService;
        private readonly CSVReaderService _passengerCsvService;

        public BookingRepository(
            CSVIOService csvioService,
            CSVReaderService bookingCsvService,
            CSVReaderService flightCsvService,
            CSVReaderService passengerCsvService
            )
        {
            _csvioService = csvioService;
            _bookingCsvService = bookingCsvService;
            _flightCsvService = flightCsvService;
            _passengerCsvService = passengerCsvService;
        }
        
        public Booking[] SearchForBookingsBy(string property, string value)
        {
            var bookings = _csvioService.GetAllRecords<Booking>(_bookingCsvService.CsvReader);
            var flightIdsFirst = bookings.Select(booking => booking.FlightId).ToArray();
            var allFlights = _csvioService.GetAllRecords<Flight>(_flightCsvService.CsvReader);
            var flightsToSearchIn = allFlights.Where(flight => flightIdsFirst.Contains(flight.Id)).ToArray();
            
            _bookingCsvService.StreamReader.Close();
            _flightCsvService.StreamReader.Close();
            
            var flightIds = flightsToSearchIn.Where(
                flight => property == "DepartureDate"
                    ? flight.GetType().GetProperty(property)?.GetValue(flight).ToString().Split(' ')[0] == value
                    : flight.GetType().GetProperty(property)?.GetValue(flight).ToString() == value).Select(flight => flight.Id);

            return bookings.Where(booking => flightIds.Contains(booking.FlightId)).ToArray();
        }
        
        public Booking[] SearchForBookingByPassenger(int passengerId)
        {
            var bookings = _csvioService.GetAllRecords<Booking>(_bookingCsvService.CsvReader);
            var passengerIds = _csvioService.GetAllRecords<Passenger>(_passengerCsvService.CsvReader)
                .Where(passenger => passenger.Id == passengerId)
                .Select(passenger => passenger.Id).ToArray();
            
            _bookingCsvService.StreamReader.Close();
            _passengerCsvService.StreamReader.Close();

            return bookings.Where(booking => passengerIds.Contains(booking.PassengerId)).ToArray();
        }

        public Booking[] GetBookings()
        {
            var bookings =  _csvioService.GetAllRecords<Booking>(_bookingCsvService.CsvReader);
            
            _bookingCsvService.StreamReader.Close();

            return bookings;
        }
    }
}