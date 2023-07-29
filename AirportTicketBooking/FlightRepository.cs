using System;

namespace AirportTicketBooking
{
    public class FlightRepository
    {
        public void InsertFlights(string absolutePathToCsvFile) {  }
        
        public Flight SearchFlightsByPrice(decimal Price)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByDepartureCountry(string departureCountry)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByDestinationCountry(string destinationCountry)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByDepartureDate(DateTime departureDate)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByDepartureAirport(string departureAirport)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByArrivalAirport(string arrivalAirport)
        {
            return new Flight();
        }
        
        public Flight SearchFlightsByFlightClass(string flightClass)
        {
            return new Flight();
        }
    }
}