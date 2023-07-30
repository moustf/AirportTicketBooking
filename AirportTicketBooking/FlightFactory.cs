using System;

namespace AirportTicketBooking
{
    public class FlightFactory
    {
        public Flight CreateNewFlight(
            int flightId,
            string flightClass,
            string departureCountry,
            string destinationCountry,
            string departureAirport,
            string arrivalAirport,
            string airlinesName,
            DateTime departureDate
        )
        {
            var flight = new Flight()
            {
                FlightId = flightId,
                FlightClass = flightClass,
                DepartureCountry = departureCountry,
                DestinationCountry = destinationCountry,
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                AirlinesName = airlinesName,
                DepartureDate = departureDate,
            };

            var flightRefType = flight.GetType();
            var flightClassRef = (string) flightRefType.GetProperty("FlightClass")?.GetValue(flight, null)!;
            var flightIdRef = flightRefType.GetProperty("FlightId")?.GetValue(flight, null)!;
            var flightDepartureCountryRef = flightRefType.GetProperty("DepartureCountry")?.GetValue(flight, null)!;
            var flightDestinationCountryRef = flightRefType.GetProperty("DestinationCountry")?.GetValue(flight, null)!;
            
            var flightPriceValue = flightClassRef == "economy"
                ? 199.99M
                : flightClassRef == "business"
                    ? 399.99M
                    : 599.99M;
            var flightNameValue =
                $"{flightIdRef}:{flightDepartureCountryRef}:{flightDestinationCountryRef}:{flightClassRef}";
            
            flightRefType.GetProperty("FlightPrice")?.SetValue(flight, flightPriceValue, null);
            flightRefType.GetProperty("FlightName")?.SetValue(flight, flightNameValue, null);

            var csvio = new CSVIO();
            // csvio.WriteDataToCsv(flight, "Flight");
            
            return flight;
        }
    }
}