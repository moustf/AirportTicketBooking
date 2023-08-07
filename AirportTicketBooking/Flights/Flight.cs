using System;
using CsvHelper.Configuration.Attributes;

namespace AirportTicketBooking.Flights
{
    public class Flight
    {
        [Index(2)]
        public string FlightClass { get; init; }
        [Index(3)]
        public decimal FlightPrice { get; set; }
        [Index(1)]
        public string FlightName { get; set; }
        [Index(0)]
        public int FlightId { get; init; }
        [Index(4)]
        public string DepartureCountry { get; init; }
        [Index(5)]
        public string DestinationCountry { get; init; }
        [Index(9)]
        public DateTime DepartureDate { get; init; }
        [Index(6)]
        public string DepartureAirport { get; init; }
        [Index(7)]
        public string ArrivalAirport { get; set; }
        [Index(8)]
        public string AirlinesName { get; set; }

        public override string ToString()
        {
            return $@"Flight name: {FlightName}, flight price: {FlightPrice}, departure airport: {DepartureAirport}, destination airport: {DestinationCountry}, and went on {DepartureDate}";
        }
    }
}