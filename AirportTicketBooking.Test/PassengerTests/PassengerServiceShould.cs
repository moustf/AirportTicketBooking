using System;
using AirportTicketBooking.Comparers;
using AirportTicketBooking.Dtos;
using AirportTicketBooking.Passengers;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBooking.Test.PassengerTests
{
    public class PassengerServiceShould
    {
        [Fact]
        public void ReturnNullReferenceExceptionWhenPassingNullPassengerDto()
        {
            var passengerService = new Mock<PassengerService>();

            var exception = Assert.Throws<NullReferenceException>(() => passengerService.Object.CreateNewPassenger(null));
            exception.Message.Should().StartWith("Object reference not set");
        }
        
        [Fact]
        public void CreateNewPassengerFromPassengerDto()
        {
            var passengerDto = new Mock<IPassengerDto>();
            var passengerService = new Mock<PassengerService>();

            var passengerOne = passengerService.Object.CreateNewPassenger(passengerDto.Object);
            var passengerTwo = passengerService.Object.CreateNewPassenger(passengerDto.Object);

            Assert.Equal(passengerOne, passengerTwo, new PassengerComparer());
        }
    }
}