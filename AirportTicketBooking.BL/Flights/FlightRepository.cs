using System;
using System.Globalization;
using System.IO;
using AirportTicketBooking.Services;

namespace AirportTicketBooking.Flights
{
    public class FlightRepository : IFlightRepository
    {
        private readonly CSVReaderService _flightCsvService;
        private readonly CSVIOService _csvioService;

        public FlightRepository(CSVReaderService flightCsvService, CSVIOService csvioService)
        {
            _flightCsvService = flightCsvService;
            _csvioService = csvioService;
        }
        
        public void InsertFlights(string filePath, IReadFromCsvFile readFromCsvFile, FileServices fileServices, FlightValidationService flightValidationService)
        {
            if (!filePath.Contains(".csv"))
            {
                throw new Exception("The file type is ont supported!");
            }

            if (File.Exists(filePath))
            {
                readFromCsvFile.RegisterFlightsData(filePath, fileServices, flightValidationService);
            }
            else
            {
                throw new FileNotFoundException("The file you are trying to access doesn't exist!");
            }
        }

        public Flight[] GetAllFlights()
        {
            var flights = _csvioService.GetAllRecords<Flight>(_flightCsvService.CsvReader);
            
            _flightCsvService.StreamReader.Close();

            return flights;
        }
        
        public Flight SearchFlightsByPrice(decimal price)
        {
            return GetSingleFlightBy("FlightPrice", price.ToString(CultureInfo.InvariantCulture));
        }
        
        public Flight SearchFlightsByDepartureCountry(string departureCountry)
        {
            return GetSingleFlightBy("DepartureCountry", departureCountry);
        }
        
        public Flight SearchFlightsByDestinationCountry(string destinationCountry)
        {
            return GetSingleFlightBy("DestinationCountry", destinationCountry);
        }
        
        public Flight SearchFlightsByDepartureDate(string departureDate)
        {
            return GetSingleFlightBy("DepartureDate", departureDate);
        }
        
        public Flight SearchFlightsByDepartureAirport(string departureAirport)
        {
            return GetSingleFlightBy("DepartureAirport", departureAirport);
        }
        
        public Flight SearchFlightsByArrivalAirport(string arrivalAirport)
        {
            return GetSingleFlightBy("DepartureAirport", arrivalAirport);
        }
        
        public Flight SearchFlightsByFlightClass(string flightClass)
        {
            return GetSingleFlightBy("DepartureAirport", flightClass);
        }

        private Flight GetSingleFlightBy(string category, string value)
        {
            var flights = _csvioService.GetAllRecords<Flight>(_flightCsvService.CsvReader);
            
            _flightCsvService.StreamReader.Close();
            
            return _csvioService.SearchForFlightBy(category, value, flights);
        }
    }
}
