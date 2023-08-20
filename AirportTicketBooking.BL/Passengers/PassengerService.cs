using AirportTicketBooking.Dtos;

namespace AirportTicketBooking.Passengers
{
    public class PassengerService
    {
        public Passenger CreateNewPassenger(IPassengerDto passengerData)
        {
            var passenger = new Passenger()
            {
                Id = passengerData.Id, 
                Name = passengerData.Name,
                Email = passengerData.Email,
                PassportNumber = passengerData.PassportNumber,
                CreditCard = passengerData.CreditCard,
            };
            
            return passenger;
        }
    }
}