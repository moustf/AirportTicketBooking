using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Managers
{
    public class ManagerService
    {
        public Manager CreateNewManager(IManagerDto managerData)
        {
            var manager = new Manager()
            {
                Id = managerData.Id,
                Name = managerData.Name,
                Email = managerData.Email,
                Username = managerData.Username
            };
            
            return manager;
        }
    }
}