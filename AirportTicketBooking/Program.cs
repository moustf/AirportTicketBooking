using System;
using System.Text.RegularExpressions;

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
                    HandleManagerCreation();
                    break;
                }
                case 2:
                {
                    HandlePassengerCreation();
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
                    Console.WriteLine("Please specify your name.");
                    var managerName = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(managerName))
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
                    
                    Console.WriteLine("Please provide the path to the flights file.");
                    var path = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(path))
                    {
                        Console.WriteLine("Please provide a valid path to the flights file.");
                        path = Console.ReadLine();
                    }
                    
                    csvio.ReadFlightsData(path);
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

        public static void HandlePassengerCreation()
        {
            Console.WriteLine("Do you have an account? 1 for having an account, and 2 for new passenger users.");
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
                    var passengerRepo = new PassengerRepository();
                    // Call the csv parsing method and get the user data from there.
                    Console.WriteLine("Please specify your name.");
                    var passengerName = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(passengerName))
                    {
                        Console.WriteLine("Please enter a valid name.");
                        passengerName = Console.ReadLine();
                    }

                    try
                    {
                        var passenger = passengerRepo.SearchForPassenger(passengerName);
                        Console.WriteLine($"{passenger.PassengerId}: {passenger.PassengerName} with the email {passenger.Email}");
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
                    var passengerFactory = new PassengerFactory();
                    passengerFactory.CreateNewPassenger();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
        }
    }
}