using CsvHelper.Configuration.Attributes;

namespace AirportTicketBooking.Passengers
{
    public class Passenger
    {
        [Index(0)]
        public int Id { get; init; }
        [Index(1)]
        public string Name { get; init; }
        [Index(2)]
        public string Email { get; init; }
        [Index(3)]
        public string PassportNumber { get; set; }
        [Index(4)]
        public string CreditCard { get; set; }
    }
}