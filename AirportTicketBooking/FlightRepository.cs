using System;
using System.Globalization;

namespace AirportTicketBooking
{
    public class FlightRepository
    {
        public void InsertFlights(string absolutePathToCsvFile)
        {
            var csvio = new CSVIO();
            csvio.ReadFlightsData(absolutePathToCsvFile);
        }

        public Flight[] GetAllFlights()
        {
            var csvio = new CSVIO();

            return csvio.GetAllFlightsData();
        }
        
        public Flight SearchFlightsByPrice()
        {
            Console.WriteLine("Please specify the price of the flight you want to filter upon. Price must be of this format [Whole Numbers].[Decimal Number]");
            var choiceTest = Console.ReadLine();
            decimal price;
            while (!decimal.TryParse(choiceTest, out price))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            var flight = GetSingleFlightBy("FlightPrice", price.ToString(CultureInfo.InvariantCulture));

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified price");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByDepartureCountry()
        {
            Console.WriteLine("Please specify the departure country of the flight you want to filter upon.");
            var departureCountry =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(departureCountry))
            {
                Console.WriteLine("Please specify a valid string value!");
                departureCountry = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DepartureCountry", departureCountry);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified departure country.");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByDestinationCountry()
        {
            Console.WriteLine("Please specify the destination country of the flight you want to filter upon.");
            var destinationCountry =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(destinationCountry))
            {
                Console.WriteLine("Please specify a valid string value!");
                destinationCountry = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DestinationCountry", destinationCountry);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified destination country.");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByDepartureDate()
        {
            Console.WriteLine("Please specify the departure date of the flight you want to filter upon.");
            Console.WriteLine("Departure date must be of this format: mm/dd/yyyy.");
            var departureDate =  Console.ReadLine();
            while (departureDate?.Split('/').Length < 0 || string.IsNullOrWhiteSpace(departureDate))
            {
                Console.WriteLine("Please specify a valid string value!");
                departureDate = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DepartureDate", departureDate);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified departure date.");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByDepartureAirport()
        {
            Console.WriteLine("Please specify the departure airport of the flight you want to filter upon.");
            var departureAirport =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(departureAirport))
            {
                Console.WriteLine("Please specify a valid string value!");
                departureAirport = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DepartureAirport", departureAirport);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified departure airport.");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByArrivalAirport()
        {
            Console.WriteLine("Please specify the arrival airport of the flight you want to filter upon.");
            var arrivalAirport =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(arrivalAirport))
            {
                Console.WriteLine("Please specify a valid string value!");
                arrivalAirport = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DepartureAirport", arrivalAirport);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified arrival airport.");
            Environment.Exit(1);
            return flight;
        }
        
        public Flight SearchFlightsByFlightClass()
        {
            Console.WriteLine("Please specify the flight class of the flight you want to filter upon.");
            var flightClass =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(flightClass))
            {
                Console.WriteLine("Please specify a valid string value!");
                flightClass = Console.ReadLine();
            }

            var flight = GetSingleFlightBy("DepartureAirport", flightClass);

            if (flight is not null) return flight;
            
            Console.WriteLine("There are no flights with the specified flight class.");
            Environment.Exit(1);
            return flight;
        }

        private Flight GetSingleFlightBy(string category, string value)
        {
            var csvio = new CSVIO();

            return csvio.SearchForFlightBy(category, value);
        }
    }
}