using CsvHelper;

namespace AirportTicketBooking.Managers
{
    public class ManagerRepository
    {
        public Manager SearchForExistingManager(string name, CSVIOService csvioService, CSVReaderService managerReaderService)
        {
            var manager = csvioService.SearchForRecord<Manager>("ManagerName", name, managerReaderService.CsvReader);
            
            managerReaderService.StreamReader.Close();

            return manager;
        }
    }
}