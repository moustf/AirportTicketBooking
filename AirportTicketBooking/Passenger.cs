using System;
using System.Collections.Generic;

namespace AirportTicketBooking
{
    public class Passenger
    {
        public Passenger()
        {
            PassengerBookings = new List<Booking>();
        }
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string Email { get; set; }
        public string PassengerNumber { get; set; }
        public string CreditCard { get; set; }
        public List<Booking> PassengerBookings { get; private set; }
        
        public void CancelBooking(DateTime bookingDate) {  }
        public void EditBooking(DateTime bookingDate) {  }
    }
}