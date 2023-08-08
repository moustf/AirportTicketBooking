using System;

namespace AirportTicketBooking.Utility
{
    public static class ConsoleOutputUtility
    {
        public static string NoDataFoundMessage(string dataName)
        {
            return $"There are no {dataName} found!";
        }

        public static string ShowManagerDetails(int id, string name, string email)
        {
            return $"{id}: {name} with the email {email}";
        }
        
        public static string ShowPassengerDetails(int id, string name, string email)
        {
            return $"{id}: {name} with the email {email}";
        }
    }
}