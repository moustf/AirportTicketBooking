namespace AirportTicketBooking.Managers
{
    public interface IManagerRepository
    {
        Manager SearchForManager(string name, ICSVIOService csvioService, CSVReaderService managerReaderService);
    }
}