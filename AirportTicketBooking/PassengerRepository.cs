using System.Collections.Generic;
using System;

namespace AirportTicketBooking
{
    public class PassengerRepository
    {
        public List<Booking> GetAllBookingsForPassenger(string passengerName)
        {
            return new List<Booking>();
        }

        public Passenger SearchForPassenger(string passengerName)
        {
            var csvio = new CSVIO();
            var passenger = csvio.SearchForPassenger(passengerName);

            if (passenger is null)
            {
                throw new NullReferenceException("The passenger name you searched for doesn't exist!");
            }

            return passenger;
        }
    }
}