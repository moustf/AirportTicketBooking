namespace AirportTicketBooking.Dtos
{
    public record PassengerDto(
        int Id,
        string Name,
        string Email,
        string PassportNumber,
        string CreditCardNumber
        );
}