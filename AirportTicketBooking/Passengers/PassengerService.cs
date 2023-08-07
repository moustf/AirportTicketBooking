using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Passengers
{
    public class PassengerService
    {
        public Passenger CreateNewPassenger(PassengerDto passengerData)
        {
            var passenger = new Passenger()
            {
                PassengerId = passengerData.Id,
                PassengerName = passengerData.Name,
                Email = passengerData.Email,
                PassportNumber = passengerData.PassportNumber,
                CreditCard = passengerData.CreditCardNumber,
            };
            
            return passenger;
        }
    }
}