using System.Collections.Generic;
using AirportTicketBooking.Passengers;

namespace AirportTicketBooking.Comparers
{
    public class PassengerComparer : IEqualityComparer<Passenger>
    {
        public bool Equals(Passenger passengerOne, Passenger passengerTwo)
        {
            if (passengerOne is null || passengerTwo is null) return false;
            
            if (ReferenceEquals(passengerOne, passengerTwo)) return true;
            
            
            return (
                        passengerOne.Id == passengerTwo.Id
                        && passengerOne.Name == passengerTwo.Name
                        && passengerOne.Email == passengerTwo.Email
                        && passengerOne.PassportNumber == passengerTwo.PassportNumber
                        && passengerOne.CreditCard == passengerTwo.CreditCard
                    );
        }

        public int GetHashCode(Passenger obj)
        { 
            return 
                obj.Id.GetHashCode() + obj.Name.GetHashCode() + obj.Email.GetHashCode() + obj.PassportNumber.GetHashCode() + obj.CreditCard.GetHashCode();
        }
    }
}