namespace AirportTicketBooking.Dtos
{
    public interface IManagerDto
    {
        int Id { get; init; }
        string Name { get; init; }
        string Email { get; init; }
        string Username { get; init; }
    }
}