using System;

namespace AirportTicketBooking
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var flightFactory = new FlightFactory();

            var flight = flightFactory.CreateNewFlight(
                1, 
                "economy", 
                "USA", 
                "UAE", 
                "Miami International Airport",
                "Dubai International Airport",
                "Qatar Airlines",
                DateTime.Now
            );

            Console.WriteLine(flight.FlightName);
            Console.WriteLine(flight.FlightPrice);
        }
    }
}