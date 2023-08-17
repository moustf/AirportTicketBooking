using System.Collections.Generic;
using AirportTicketBooking.Bookings;

namespace AirportTicketBooking.Comparers
{
    public class BookingComparer : IEqualityComparer<Booking>
    {
        public bool Equals(Booking bookingOne, Booking bookingTwo)
        {
            if (bookingOne is null || bookingTwo is null) return false;
            
            if (ReferenceEquals(bookingOne, bookingTwo)) return true;
            
            
            return (
                bookingOne.BookingId == bookingTwo.BookingId
                && bookingOne.FlightId == bookingTwo.FlightId
                && bookingOne.PassengerId == bookingTwo.PassengerId
                && bookingOne.DateOfBooking == bookingTwo.DateOfBooking
                && bookingOne.SeatsNumber == bookingTwo.SeatsNumber
            );
        }

        public int GetHashCode(Booking obj)
        { 
            return 
                obj.BookingId.GetHashCode() + obj.FlightId.GetHashCode() + obj.PassengerId.GetHashCode() + obj.DateOfBooking.GetHashCode() + obj.SeatsNumber.GetHashCode();
        }
    }
}