using System;
using System.Collections.Generic;

namespace AirportTicketBooking
{
    public class ManagerFactory
    {
        public Manager CreateNewManager()
        {
            var rnd = new Random();
            var csvio = new CSVIO();
            
            Console.WriteLine("Please specify your preferred id, hit enter if you can't decide.");
            var strId = Console.ReadLine();
            var managerId = string.IsNullOrWhiteSpace(strId) ? rnd.Next(100, 1000) : int.Parse(strId);

            Console.WriteLine("Please specify your name.");
            var managerName = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(managerName))
            {
                Console.WriteLine("Please specify a valid name!");
                managerName = Console.ReadLine();
            }
            
            Console.WriteLine("Please specify your email.");
            var managerEmail = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(managerEmail))
            {
                Console.WriteLine("Please specify a valid email!");
                managerEmail = Console.ReadLine();
            }
            
            Console.WriteLine("Please specify your username.");
            var managerUsername = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(managerUsername))
            {
                Console.WriteLine("Please specify a valid username!");
                managerUsername = Console.ReadLine();
            }

            var manager = new Manager()
            {
                ManagerId = managerId,
                ManagerName = managerName,
                Email = managerEmail,
                Username = managerUsername
            };
            
            csvio.WriteDataToCsv(manager, "Manager");

            return manager;
        }
    }
}