using System;
using System.Text.RegularExpressions;

namespace AirportTicketBooking
{
    public class PassengerFactory
    {
        public void CreateNewPassenger()
        {
            var rnd = new Random();
            var csvio = new CSVIO();
            
            Console.WriteLine("Please specify your preferred id, hit enter if you can't decide.");
            var strId = Console.ReadLine();
            var passengerId = string.IsNullOrWhiteSpace(strId) ? rnd.Next(100, 1000) : int.Parse(strId);

            Console.WriteLine("Please specify your name.");
            var passengerName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(passengerName))
            {
                Console.WriteLine("Please specify a valid name!");
                passengerName = Console.ReadLine();
            }
            
            Console.WriteLine("Please specify your email.");
            var passengerEmail = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(passengerEmail))
            {
                Console.WriteLine("Please specify a valid email!");
                passengerEmail = Console.ReadLine();
            }
            
            Console.WriteLine("Please specify your passport number. Passport numbers must be of 9 digits.");
            var passportNumber = Console.ReadLine();
            while (!Regex.Match(passportNumber ?? "", @"^[0-9]{9}$").Success || string.IsNullOrWhiteSpace(passportNumber))
            {
                Console.WriteLine("Please specify a valid passport number!");
                passportNumber = Console.ReadLine();
            }
            
            Console.WriteLine("Please specify your credit card number. Credit card numbers must be of 12 digits.");
            var creditCardNumber = Console.ReadLine();
            while (!Regex.Match(creditCardNumber ?? "", @"^[0-9]{12}$").Success || string.IsNullOrWhiteSpace(creditCardNumber))
            {
                Console.WriteLine("Please specify a valid credit card number!");
                creditCardNumber = Console.ReadLine();
            }

            var passenger = new Passenger()
            {
                PassengerId = passengerId,
                PassengerName = passengerName,
                Email = passengerEmail,
                PassportNumber = passportNumber,
                CreditCard = creditCardNumber,
            };
            
            csvio.WriteDataToCsv(passenger, "Passenger");
        }
    }
}