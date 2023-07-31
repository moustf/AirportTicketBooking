using System;
using System.Linq;
using System.Reflection;
using CsvHelper.Configuration.Attributes;

namespace AirportTicketBooking
{
    public class Flight
    {
        private string _flightClass;
        [Index(2)]
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
        [Index(3)]
        public decimal FlightPrice { get; set; }
        [Index(1)]
        public string FlightName { get; private set; }
        [Index(0)]
        public int FlightId { get; set; }
        [Index(4)]
        public string DepartureCountry { get; set; }
        [Index(5)]
        public string DestinationCountry { get; set; }
        [Index(9)]
        public DateTime DepartureDate { get; set; }
        [Index(6)]
        public string DepartureAirport { get; set; }
        [Index(7)]
        public string ArrivalAirport { get; set; }
        [Index(8)]
        public string AirlinesName { get; set; }
    }
}