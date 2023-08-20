using CsvHelper;

namespace AirportTicketBooking.Managers
{
    public class ManagerRepository : IManagerRepository
    {
        public Manager SearchForManager(string name, ICSVIOService csvioService, CSVReaderService managerReaderService)
        {
            var manager = csvioService.SearchForRecord<Manager>("ManagerName", name, managerReaderService.CsvReader);
            
            managerReaderService.StreamReader.Close();

            return manager;
        }
    }
}