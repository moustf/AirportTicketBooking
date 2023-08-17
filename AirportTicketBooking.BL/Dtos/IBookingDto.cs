using System;

namespace AirportTicketBooking.Dtos
{
    public interface IBookingDto
    {
        int Id { get; init; }
        int FlightId { get; init; }
        int PassengerId { get; init; }
        int SeatsNum { get; init; }
        DateTime DateOfBooking { get; init; }
    }
}