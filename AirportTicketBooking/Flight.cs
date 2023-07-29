using System;
using System.Linq;
using System.Reflection;

namespace AirportTicketBooking
{
    public class Flight
    {
        private string _flightClass;
        public string FlightClass
        {
            get => _flightClass;
            set
            {
                var classes = new string[] { "economy", "business", "first class", };
                if (!classes.Contains(value))
                {
                    throw new Exception("Flight class can be 'economy', 'business', or 'first class'.");
                }

                _flightClass = value;
            }
        }

        public decimal FlightPrice { get; set; }
        public string FlightName { get; private set; }
        public int FlightId { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string AirlinesName { get; set; }
    }
}