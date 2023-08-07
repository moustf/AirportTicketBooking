using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Managers
{
    public class ManagerService
    {
        public Manager CreateNewManager(ManagerDto managerData)
        {
            var manager = new Manager()
            {
                ManagerId = managerData.Id,
                ManagerName = managerData.Name,
                Email = managerData.Email,
                Username = managerData.Username
            };
            
            return manager;
        }
    }
}