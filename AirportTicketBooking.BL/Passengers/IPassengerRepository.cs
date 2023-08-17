using CsvHelper;

namespace AirportTicketBooking.Passengers
{
    public interface IPassengerRepository
    {
        Passenger SearchForPassenger(string passengerName, ICSVIOService csvioService, CsvReader csvReader);
    }
}