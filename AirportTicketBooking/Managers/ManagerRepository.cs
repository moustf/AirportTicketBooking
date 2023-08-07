using CsvHelper;

namespace AirportTicketBooking.Managers
{
    public class ManagerRepository
    {
        public Manager SearchForExistingManager(string name, CSVIOService csvioService, CsvReader csvReader)
        {
            var manager = csvioService.SearchForRecord<Manager>("ManagerName", name, csvReader);

            return manager;
        }
    }
}