using AirportTicketBooking.Services;

namespace AirportTicketBooking
{
    public interface IReadFromCsvFile
    {
        void RegisterFlightsData(string filePath, FileServices fileServices,
            FlightValidationService flightValidationService);
        
    }
}