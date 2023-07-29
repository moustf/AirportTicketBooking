using System.Collections.Generic;
using System;

namespace AirportTicketBooking
{
    public class Booking
    {
        private DateTime _dateOfBooking;
        public DateTime DateOfBooking
        {
            get => _dateOfBooking;
            set => _dateOfBooking = value.Date;
        }
        public List<Flight> Flights { get; set; }
        public int SeatsNumber { get; set; }
    }
}