using System;
using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Utility
{
    public static class GetBookingData
    {
        private static readonly Random Random = new Random();

        public static BookingDto GetBookingDataFromUser()
        {
            Console.WriteLine("Please specify your preferred id, hit enter if you can't decide.");
            var strId = Console.ReadLine();
            var bookingId = string.IsNullOrWhiteSpace(strId) ? Random.Next(100, 1000) : int.Parse(strId);

            Console.WriteLine("Please specify the id of the flight you chose.");
            var testId  = Console.ReadLine();
            int flightId;
            while (!int.TryParse(testId, out flightId))
            {
                Console.WriteLine("Please specify a valid integer flight id!");
                testId = Console.ReadLine();
            }

            Console.WriteLine("Please specify the id of your account id which appeared to you when you registered or search for your data.");
            testId  = Console.ReadLine();
            int passengerId;
            while (!int.TryParse(testId, out passengerId))
            {
                Console.WriteLine("Please specify a valid integer passenger id!");
                testId = Console.ReadLine();
            }

            Console.WriteLine("Please specify the number of seats you want to book.");
            testId  = Console.ReadLine();
            int seatsNumber;
            while (!int.TryParse(testId, out seatsNumber))
            {
                Console.WriteLine("Please specify a valid number of seats!");
                testId = Console.ReadLine();
            }

            return new BookingDto(bookingId, flightId, passengerId, seatsNumber, DateTime.Now);
        }
    }
}