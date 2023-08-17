using CsvHelper;

namespace AirportTicketBooking.Passengers
{
    public class PassengerRepository
    {
        public Passenger SearchForPassenger(string passengerName, CSVIOService csvioService, CsvReader csvReader)
        {
            var passenger = csvioService.SearchForRecord<Passenger>("PassengerName", passengerName, csvReader);

            return passenger;
        }
    }
}