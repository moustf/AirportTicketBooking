using System;

namespace AirportTicketBooking
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Ticket Booking System!");
            Console.WriteLine("What is you role? 1 for Manager, and 2 for Passenger.");
            var roleTest = Console.ReadLine();
            int role;
            
            while (!int.TryParse(roleTest, out role))
            {
                Console.WriteLine("Please specify a valid number!");
                roleTest = Console.ReadLine();
            }

            switch (role)
            {
                case 1:
                {
                    // Ask manager if he is old or new, then do the logic.
                    // if the manager is new, create new object and add it to the file.
                    // if the manager is old, parse the file, and get his data.
                    HandleManagerCreation();
                    break;
                }
                case 2:
                {
                    // Ask passenger if he is old or new, then do the logic.
                    // if the passenger is new, create new object and add it to the file.
                    // if the passenger is old, parse the file, and get his data.
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user role!");
                    Environment.Exit(1);
                    break;
                }
            }
        }

        public static void HandleManagerCreation()
        {
            Console.WriteLine("Do you have an account? 1 for having an account, and 2 for new manager users.");
            var choiceTest = Console.ReadLine();
            int choice;

            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            switch (choice)
            {
                case 1:
                {
                    var managerRepo = new ManagerRepository();
                    // Call the csv parsing method and get the user data from there.
                    Console.WriteLine("Please specify the your name.");
                    var managerName = Console.ReadLine();

                    while (String.IsNullOrWhiteSpace(managerName))
                    {
                        Console.WriteLine("Please enter a valid name.");
                        managerName = Console.ReadLine();
                    }

                    try
                    {
                        var manager = managerRepo.SearchForExistingManager(managerName);
                        Console.WriteLine($"{manager.ManagerId}: {manager.ManagerName} with the email {manager.Email}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Environment.Exit(1);
                    }
                    break;
                }
                case 2:
                {
                    // Create new manager object and add it to the managers file.
                    var managerFactory = new ManagerFactory();
                    managerFactory.CreateNewManager();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
            
            // TODO: specify other options like reading a csv flights from a file
            // TODO: tell the user to put the file in the location you going to print to him and specify the file name.
            // TODO: other options includes filtering the bookings by the criteria. This can wait till you get the passenger functionality done.
            Console.WriteLine("Do you want to do other operations?");
            Console.WriteLine("1 --> Import flights data from a CSV file.");
            Console.WriteLine("0 --> Exit.");
            
            choiceTest = Console.ReadLine();
            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            switch (choice)
            {
                case 0:
                {
                    Environment.Exit(0);
                    break;
                }
                case 1:
                {
                    var csvio = new CSVIO();
                    csvio.ReadFlightsData(@"/home/moustf/loving_backend/c#/AirportTicketBooking/AirportTicketBooking/DataStore/Flight.csv");
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid choice value, please choose on of the options numbers.");
                    Environment.Exit(1);
                    break;
                }
            }
        }
    }
}