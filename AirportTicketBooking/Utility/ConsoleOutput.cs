using System;

namespace AirportTicketBooking.Utility
{
    public static class ConsoleOutput
    {
        public static void NoDataFoundMessage(string dataName)
        {
            Console.WriteLine($"There are no {dataName} found!");
        }

        public static void ShowManagerDetails(int id, string name, string email)
        {
            Console.WriteLine($"{id}: {name} with the email {email}");
        }
        
        public static void ShowPassengerDetails(int id, string name, string email)
        {
            Console.WriteLine($"{id}: {name} with the email {email}");
        }

        public static void PrintGeneralMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}