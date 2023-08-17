using System;

namespace AirportTicketBooking.Dtos
{
    public record BookingDto(int Id, int FlightId, int PassengerId, int SeatsNum, DateTime DateOfBooking);
}