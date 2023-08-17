using System;
using AirportTicketBooking.Comparers;
using AirportTicketBooking.Dtos;
using AirportTicketBooking.Managers;
using AirportTicketBooking.Passengers;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBooking.Test.ManagerTests
{
    public class ManagerServiceShould
    {
        [Fact]
        public void ReturnNullReferenceExceptionWhenPassingNullManagerDto()
        {
            var managerService = new Mock<ManagerService>();

            var exception = Assert.Throws<NullReferenceException>(() => managerService.Object.CreateNewManager(null));
            exception.Message.Should().StartWith("Object reference not set");
        }
        
        [Fact]
        public void CreateNewManagerFromManagerDto()
        {
            var managerDto = new Mock<IManagerDto>();
            var managerService = new Mock<ManagerService>();

            var managerOne = managerService.Object.CreateNewManager(managerDto.Object);
            var managerTwo = managerService.Object.CreateNewManager(managerDto.Object);

            Assert.Equal(managerOne, managerTwo, new ManagerComparer());
        }
    }
}