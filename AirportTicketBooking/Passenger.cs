using CsvHelper.Configuration.Attributes;

namespace AirportTicketBooking
{
    public class Passenger
    {
        [Index(0)]
        public int PassengerId { get; set; }
        [Index(1)]
        public string PassengerName { get; set; }
        [Index(2)]
        public string Email { get; set; }
        [Index(3)]
        public string PassportNumber { get; set; }
        [Index(4)]
        public string CreditCard { get; set; }
    }
}