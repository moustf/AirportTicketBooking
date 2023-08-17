using AirportTicketBooking.Services;

namespace AirportTicketBooking.Flights
{
    public interface IFlightRepository
    {
        void InsertFlights(string filePath, IReadFromCsvFile readFromCsvFile, FileServices fileServices,
            FlightValidationService flightValidationService);

        Flight[] GetAllFlights();
        Flight SearchFlightsByPrice(decimal price);
        Flight SearchFlightsByDepartureCountry(string departureCountry);
        Flight SearchFlightsByDestinationCountry(string destinationCountry);
        Flight SearchFlightsByDepartureDate(string departureDate);
        Flight SearchFlightsByDepartureAirport(string departureAirport);
        Flight SearchFlightsByArrivalAirport(string arrivalAirport);
        Flight SearchFlightsByFlightClass(string flightClass);
    }
}