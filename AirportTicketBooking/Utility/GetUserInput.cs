using System;
using System.Text.RegularExpressions;
using AirportTicketBooking.Enums;

namespace AirportTicketBooking.Utility
{
    public static class GetUserInput
    {
        private static readonly Random Random = new Random();
        
        public static decimal GetDecimalData(string name)
        {
            Console.WriteLine($"Please specify the {name} you want to filter upon. Price must be of this format [Whole Numbers].[Decimal Number]");
            var choiceTest = Console.ReadLine();
            decimal num;
            while (!decimal.TryParse(choiceTest, out num))
            {
                Console.WriteLine("Please specify a valid decimal number!");
            }

            return num;
        }

        public static string GetStringData(string name)
        {
            Console.WriteLine($"Please specify the {name}.");
            var value =  Console.ReadLine();
            while (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Please specify a valid string value!");
                value = Console.ReadLine();
            }

            return value;
        }

        public static string GetDateOnlyData(string name)
        {
            Console.WriteLine($"Please specify the {name} you want to filter upon.");
            Console.WriteLine("Date values must be of this format: mm/dd/yyyy.");
            var value =  Console.ReadLine();
            while (value?.Split('/').Length < 0 || string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Please specify a valid date value!");
                value = Console.ReadLine();
            }

            return value;
        }

        public static int GetIdFromUser()
        {
            Console.WriteLine("Please specify your preferred id, hit enter if you can't decide.");
            var strId = Console.ReadLine();
            var id = string.IsNullOrWhiteSpace(strId) ? Random.Next(100, 1000) : int.Parse(strId);

            return id;
        }

        public static int GetIntegerFromUser(string name)
        {
            Console.WriteLine($"Please specify the {name}");
            var choiceTest = Console.ReadLine();
            int num;
            while (!int.TryParse(choiceTest, out num))
            {
                Console.WriteLine("Please specify a valid integer number!");
            }

            return num;
        }

        public static string GetPassportNumber()
        {
            Console.WriteLine("Please specify your passport number. Passport numbers must be of 9 digits.");
            var passportNumber = Console.ReadLine();
            while (!Regex.Match(passportNumber ?? "", @"^[0-9]{9}$").Success || string.IsNullOrWhiteSpace(passportNumber))
            {
                Console.WriteLine("Please specify a valid passport number!");
                passportNumber = Console.ReadLine();
            }

            return passportNumber;
        }

        public static string GetCreditCardNumber()
        {
            Console.WriteLine("Please specify your credit card number. Credit card numbers must be of 12 digits.");
            var creditCardNumber = Console.ReadLine();
            while (!Regex.Match(creditCardNumber ?? "", @"^[0-9]{12}$").Success || string.IsNullOrWhiteSpace(creditCardNumber))
            {
                Console.WriteLine("Please specify a valid credit card number!");
                creditCardNumber = Console.ReadLine();
            }

            return creditCardNumber;
        }

        public static string GetFlightClassCategory()
        {
            Console.WriteLine("What is the flight class of the flights you want to search with?");
            Console.WriteLine("Flight classes can either be 'economy', 'business', or 'first class'.");
            var flightClass = Console.ReadLine();

            while (flightClass != "economy" || flightClass != "business" ||  flightClass != "first class")
            {
                Console.WriteLine("Please specify a valid flight class!");
                flightClass = Console.ReadLine();
            }

            return flightClass;
        }
        
        public static UserRoles? GetUserRole()
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

            return role == 1 ? UserRoles.Manager : role == 2 ? UserRoles.Passenger : null;
        }

        public static DoesAccountExist? GetUserAccountStatus()
        {
            Console.WriteLine("Do you have an account? 1 for having an account, and 2 for new users.");
            var choiceTest = Console.ReadLine();
            int choice;

            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            return choice == 1 ? DoesAccountExist.Yes : choice == 2 ? DoesAccountExist.No : null;
        }

        public static ManagerOperations? GetManagerOperation()
        {
            Console.WriteLine("Do you want to do other operations?");
            Console.WriteLine("1 --> Import flights data from a CSV file.");
            Console.WriteLine("2 --> Search for a booking by the flight id.");
            Console.WriteLine("3 --> Search for a booking by the flight price.");
            Console.WriteLine("4 --> Search for a booking by the departure country.");
            Console.WriteLine("5 --> Search for a booking by the destination country.");
            Console.WriteLine("6 --> Search for a booking by the departure date.");
            Console.WriteLine("7 --> Search for a booking by the departure airport.");
            Console.WriteLine("8 --> Search for a booking by the arrival airport.");
            Console.WriteLine("9 --> Search for a booking by the flight class.");
            Console.WriteLine("10 --> Search for a booking by the passenger id.");
            Console.WriteLine("0 --> Exit.");
            
            var choiceTest = Console.ReadLine();
            int choice;
            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            return choice switch
            {
                1 => ManagerOperations.ImportFlights,
                2 => ManagerOperations.SearchByFlightId,
                3 => ManagerOperations.SearchByFlightPrice,
                4 => ManagerOperations.SearchByDepartureCountry,
                5 => ManagerOperations.SearchByDestinationCountry,
                6 => ManagerOperations.SearchByDepartureDate,
                7 => ManagerOperations.SearchByDepartureAirport,
                8 => ManagerOperations.SearchByArrivalAirport,
                9 => ManagerOperations.SearchByFlightClass,
                10 => ManagerOperations.SearchByPassengerId,
                0 => ManagerOperations.Exit,
                _ => null
            };
        }

        public static PassengerOperations? GetPassengerOperations()
        {
            Console.WriteLine("Do you want to do other operations?");
            Console.WriteLine("Please note that in order to book a flight, you need to remember it's flight id and passenger id(your id).");
            Console.WriteLine("1 --> Get all the flights.");
            Console.WriteLine("2 --> Search for a flight by the price.");
            Console.WriteLine("3 --> Search for a flight by the departure country.");
            Console.WriteLine("4 --> Search for a flight by the destination country.");
            Console.WriteLine("5 --> Search for a flight by the departure date.");
            Console.WriteLine("6 --> Search for a flight by the departure airport.");
            Console.WriteLine("7 --> Search for a flight by the arrival airport.");
            Console.WriteLine("8 --> Search for a flight by the flight class.");
            Console.WriteLine("9 --> Make a booking.");
            Console.WriteLine("10 --> Cancel a booking.");
            Console.WriteLine("11 --> Modify a booking.");
            Console.WriteLine("12 --> Get all bookings.");
            Console.WriteLine("0 --> Exit.");
            
            var choiceTest = Console.ReadLine();
            int choice;
            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            return choice switch
            {
                0 => PassengerOperations.Exit,
                1 => PassengerOperations.GetFlights,
                2 => PassengerOperations.SearchByFlightPrice,
                3 => PassengerOperations.SearchByDepartureCountry,
                4 => PassengerOperations.SearchByDestinationCountry,
                5 => PassengerOperations.SearchByDepartureDate,
                6 => PassengerOperations.SearchByDepartureAirport,
                7 => PassengerOperations.SearchByArrivalAirport,
                8 => PassengerOperations.SearchByFlightClass,
                9 => PassengerOperations.MakeABooking,
                10 => PassengerOperations.CancelABooking,
                11 => PassengerOperations.ModifyABooking,
                12 => PassengerOperations.GetBookings,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}