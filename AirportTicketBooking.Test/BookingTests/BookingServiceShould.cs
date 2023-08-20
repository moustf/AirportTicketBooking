using System;
using AirportTicketBooking.Bookings;
using AirportTicketBooking.Comparers;
using AirportTicketBooking.Dtos;
using AirportTicketBooking.Passengers;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBooking.Test.BookingTests
{
    public class BookingServiceShould
    {
        [Fact]
        public void ReturnNullReferenceExceptionWhenPassingNullBookingDto()
        {
            var bookingService = new Mock<BookingService>();

            var exception = Assert.Throws<NullReferenceException>(() => bookingService.Object.CreateNewBooking(null));
            exception.Message.Should().StartWith("Object reference not set");
        }
        
        [Fact]
        public void CreateNewPassengerFromBookingDto()
        {
            var bookingDto = new Mock<IBookingDto>();
            var bookingService = new Mock<BookingService>();

            var passengerOne = bookingService.Object.CreateNewBooking(bookingDto.Object);
            var passengerTwo = bookingService.Object.CreateNewBooking(bookingDto.Object);

            Assert.Equal(passengerOne, passengerTwo, new BookingComparer());
        }
    }
}