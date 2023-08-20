using CsvHelper;

namespace AirportTicketBooking.Passengers
{
    public class PassengerRepository : IPassengerRepository
    {
        public Passenger SearchForPassenger(string passengerName, ICSVIOService csvioService, CsvReader csvReader)
        {
            var passenger = csvioService.SearchForRecord<Passenger>("Name", passengerName, csvReader);

            return passenger;
        }
    }
}