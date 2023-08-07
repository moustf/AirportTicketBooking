using CsvHelper.Configuration.Attributes;

namespace AirportTicketBooking.Managers
{
    public class Manager
    {
        [Name("ManagerId")]
        [Index(0)]
        public int ManagerId { get; set; }
        [Index(1)]
        [Name("ManagerName")]
        public string ManagerName { get; set; }
        [Index(2)]
        [Name("Email")]
        public string Email { get; set; }
        [Index(3)]
        [Name("Username")]
        public string Username { get; set; }
    }
}