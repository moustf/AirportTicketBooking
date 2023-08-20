using AirportTicketBooking.Comparers;
using AirportTicketBooking.Passengers;
using Moq;
using Xunit;

namespace AirportTicketBooking.Test.PassengerTests
{
    public class PassengerRepositoryShould
    {
        [Fact]
        public void ReturnExistingPassenger()
        {
            var csvioService = new Mock<ICSVIOService>();
            var csvioReader = new Mock<CSVReaderService>();
            var passengerRepository = new Mock<IPassengerRepository>();
            var passenger = new Mock<Passenger>();
            
            passengerRepository
                .Setup(passengerRepo => 
                    passengerRepo.SearchForPassenger(It.IsAny<string>(), csvioService.Object, csvioReader.Object.CsvReader)
                    )
                .Returns(passenger.Object);

            var expected =
                passengerRepository.Object.SearchForPassenger(It.IsAny<string>(), csvioService.Object, csvioReader.Object.CsvReader);
            
            Assert.Equal(passenger.Object, expected, new PassengerComparer());
        }
        [Fact]
        public void ReturnNullWhenPassingNonExistingPassengerName()
        {
            var csvioService = new Mock<ICSVIOService>();
            var csvioReader = new Mock<CSVReaderService>();
            var passengerRepository = new Mock<IPassengerRepository>();
            
            passengerRepository
                .Setup(passengerRepo => 
                    passengerRepo.SearchForPassenger(It.IsAny<string>(), csvioService.Object, csvioReader.Object.CsvReader)
                )
                .Returns((Passenger)null);

            var expected =
                passengerRepository.Object.SearchForPassenger(It.IsAny<string>(), csvioService.Object, csvioReader.Object.CsvReader);
            
            Assert.Equal(null, expected);
        }
    }
}