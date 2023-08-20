namespace AirportTicketBooking.Dtos
{
    public interface IPassengerDto
    {
        int Id { get; init; }
        string Name { get; init; }
        string Email { get; init; }
        string PassportNumber { get; init; }
        string CreditCard { get; init; }
    }
}