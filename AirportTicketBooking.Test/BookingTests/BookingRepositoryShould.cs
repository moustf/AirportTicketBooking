using System;
using AirportTicketBooking.Bookings;
using AutoFixture;
using Moq;
using Xunit;

namespace AirportTicketBooking.Test.BookingTests
{
    public class BookingRepositoryShould
    {
        [Fact]
        public void ReturnArrayOfBookingsByPropertyName()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            var fixture = new Fixture();
            var bookings = fixture.Create<Booking[]>();
            const string property = "BookingId";
            const string value = "1";

            bookingRepository
                .Setup(bookingRepo => bookingRepo.SearchForBookingsBy(property, value))
                .Returns(bookings);

            var returnedBookings = bookingRepository.Object.SearchForBookingsBy(property, value);
            
            Assert.Equal(bookings.Length, returnedBookings.Length);
            Assert.Equal(bookings[0].BookingId, returnedBookings[0].BookingId);
        }
        
        [Fact]
        public void ReturnEmptyArrayWhenPropertyValueDoesNotExist()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            const string property = "BookingId";
            const string value = "1100";

            bookingRepository
                .Setup(bookingRepo => bookingRepo.SearchForBookingsBy(property, value))
                .Returns(Array.Empty<Booking>());

            var returnedBookings = bookingRepository.Object.SearchForBookingsBy(property, value);
            
            Assert.Equal(0, returnedBookings.Length);
        }
        
        [Fact]
        public void ReturnArrayOfBookingsByPassengerId()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            var fixture = new Fixture();
            var bookings = fixture.Create<Booking[]>();
            const int id = 1;

            bookingRepository
                .Setup(bookingRepo => bookingRepo.SearchForBookingByPassenger(id))
                .Returns(bookings);

            var returnedBookings = bookingRepository.Object.SearchForBookingByPassenger(id);
            
            Assert.Equal(bookings.Length, returnedBookings.Length);
            Assert.Equal(bookings[0].BookingId, returnedBookings[0].BookingId);
        }
        
        [Fact]
        public void ReturnEmptyArrayWhenPassengerIdDoesNotExist()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            const int id = 1100;

            bookingRepository
                .Setup(bookingRepo => bookingRepo.SearchForBookingByPassenger(id))
                .Returns(Array.Empty<Booking>());

            var returnedBookings = bookingRepository.Object.SearchForBookingByPassenger(id);
            
            Assert.Equal(0, returnedBookings.Length);
        }
        
        [Fact]
        public void ReturnArrayOfAllBookings()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            var fixture = new Fixture();
            var bookings = fixture.Create<Booking[]>();

            bookingRepository
                .Setup(bookingRepo => bookingRepo.GetBookings())
                .Returns(bookings);

            var returnedBookings = bookingRepository.Object.GetBookings();
            
            Assert.Equal(bookings.Length, returnedBookings.Length);
            Assert.Equal(bookings[0].BookingId, returnedBookings[0].BookingId);
        }
        
        [Fact]
        public void ReturnEmptyArrayWhenNoBookingsExist()
        {
            var bookingRepository = new Mock<IBookingRepository>();

            bookingRepository
                .Setup(bookingRepo => bookingRepo.GetBookings())
                .Returns(Array.Empty<Booking>());

            var returnedBookings = bookingRepository.Object.GetBookings();
            
            Assert.Equal(0, returnedBookings.Length);
        }
    }
}