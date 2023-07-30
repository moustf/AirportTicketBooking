using System;

namespace AirportTicketBooking
{
    public class ManagerRepository
    {
        public Manager SearchForExistingManager(string name)
        {
            var csvio = new CSVIO();
            var manager = csvio.SearchForManager(name);

            if (manager is null)
            {
                throw new NullReferenceException("The manager name you searched for doesn't exist!");
            }

            return manager;
        }
    }
}