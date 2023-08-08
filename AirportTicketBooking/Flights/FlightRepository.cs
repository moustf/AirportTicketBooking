using System;
using System.Globalization;
using System.IO;
using AirportTicketBooking.Services;
using CsvHelper;

namespace AirportTicketBooking.Flights
{
    public class FlightRepository
    {
        private readonly CSVReaderService _flightCsvService;
        private readonly CSVIOService _csvioService;

        public FlightRepository(CSVReaderService flightCsvService, CSVIOService csvioService)
        {
            _flightCsvService = flightCsvService;
            _csvioService = csvioService;
        }
        
        public void InsertFlights(string filePath, ReadFromCsvFile readFromCsvFile, FileServices fileServices, Validation validation)
        {
            if (!filePath.Contains(".csv"))
            {
                throw new Exception("The file type is ont supported!");
            }

            if (File.Exists(filePath))
            {
                readFromCsvFile.RegisterFlightsData(filePath, fileServices, validation);
            }
            else
            {
                throw new NullReferenceException("The file you are trying to access doesn't exist!");
            }
        }

        public Flight[] GetAllFlights()
        {
            var flights = _csvioService.GetAllRecords<Flight>("Flight", _flightCsvService.CsvReader);
            
            _flightCsvService.StreamReader.Close();

            return flights;
        }
        
        public Flight SearchFlightsByPrice(decimal price)
        {
            var flight = GetSingleFlightBy("FlightPrice", price.ToString(CultureInfo.InvariantCulture));

            return flight;
        }
        
        public Flight SearchFlightsByDepartureCountry(string departureCountry)
        {
            var flight = GetSingleFlightBy("DepartureCountry", departureCountry);
            
            return flight;
        }
        
        public Flight SearchFlightsByDestinationCountry(string destinationCountry)
        {
            var flight = GetSingleFlightBy("DestinationCountry", destinationCountry);

            return flight;
        }
        
        public Flight SearchFlightsByDepartureDate(string departureDate)
        {
            var flight = GetSingleFlightBy("DepartureDate", departureDate);

            return flight;
        }
        
        public Flight SearchFlightsByDepartureAirport(string departureAirport)
        {
            var flight = GetSingleFlightBy("DepartureAirport", departureAirport);
            
            return flight;
        }
        
        public Flight SearchFlightsByArrivalAirport(string arrivalAirport)
        {
            var flight = GetSingleFlightBy("DepartureAirport", arrivalAirport);

            return flight;
        }
        
        public Flight SearchFlightsByFlightClass(string flightClass)
        {
            var flight = GetSingleFlightBy("DepartureAirport", flightClass);

            return flight;
        }

        private Flight GetSingleFlightBy(string category, string value)
        {
            var flights = _csvioService.GetAllRecords<Flight>("Flight", _flightCsvService.CsvReader);
            
            _flightCsvService.StreamReader.Close();
            
            return _csvioService.SearchForFlightBy(category, value, flights);
        }
    }
}
