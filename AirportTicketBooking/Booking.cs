using System;

namespace AirportTicketBooking
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime DateOfBooking { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public int SeatsNumber { get; set; }
        public void CancelBooking(int bookingId)
        {
            var csvio = new CSVIO();
            
            csvio.RemoveBooking(bookingId);
        }

        public Booking EditBooking(int bookingId)
        {
            var csvio = new CSVIO();

            return csvio.SearchForRecord<Booking>("BookingId", bookingId.ToString(), "Booking");
        }

        public override string ToString()
        {
            return $@"A booking with an id of: {BookingId}, and booked in {DateOfBooking}, with {SeatsNumber} of seats is available!";
        }
    }
}