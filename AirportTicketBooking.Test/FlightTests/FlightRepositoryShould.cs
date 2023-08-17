using System;
using System.IO;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Services;
using Xunit;
using AutoFixture;
using Moq;

namespace AirportTicketBooking.Test.FlightTests
{
    public class FlightRepositoryShould
    {
        [Fact]
        public void InsertFlightsWhenValidFilePath()
        {
            var readFromCsv = new Mock<IReadFromCsvFile>();
            var fileService = new Mock<FileServices>();
            var flightValidationService = new Mock<FlightValidationService>();
            var flightRepository = new Mock<IFlightRepository>();
            const string filePath = "any.csv";
            
            readFromCsv.Setup(reader =>
                reader.RegisterFlightsData(filePath, fileService.Object, flightValidationService.Object));
            
            readFromCsv.Object.RegisterFlightsData(filePath, fileService.Object, flightValidationService.Object);
            
            var exception = Record.Exception(
                () => flightRepository.Object.InsertFlights("any.csv", readFromCsv.Object, fileService.Object, flightValidationService.Object)
            );
            
            Assert.Null(exception);
            readFromCsv.Verify(
                reader => reader.RegisterFlightsData(filePath, fileService.Object, flightValidationService.Object),
                Times.Once
                );
        }

        [Fact]
        public void ThrowFileNotFoundExceptionWhenFileNotFound()
        {
            var readFromCsv = new Mock<IReadFromCsvFile>();
            var fileService = new Mock<FileServices>();
            var flightValidationService = new Mock<FlightValidationService>();
            var flightRepository = new Mock<IFlightRepository>();
            const string filePath = "any.csv";

            flightRepository.Setup(flightRepo => flightRepo.InsertFlights(filePath, readFromCsv.Object,
                    fileService.Object,
                    flightValidationService.Object))
                .Throws<FileNotFoundException>();

            Assert.Throws<FileNotFoundException>(() =>
                flightRepository.Object.InsertFlights(filePath, readFromCsv.Object, fileService.Object,
                    flightValidationService.Object));
            
            readFromCsv.VerifyNoOtherCalls();
            fileService.VerifyNoOtherCalls();
            flightValidationService.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void ThrowExceptionWhenInvalidFilePath()
        {
            var readFromCsv = new Mock<IReadFromCsvFile>();
            var fileService = new Mock<FileServices>();
            var flightValidationService = new Mock<FlightValidationService>();
            var flightRepository = new Mock<IFlightRepository>();
            const string filePath = "any.txt";

            flightRepository.Setup(flightRepo => flightRepo.InsertFlights(filePath, readFromCsv.Object,
                    fileService.Object,
                    flightValidationService.Object))
                .Throws<Exception>();

            Assert.Throws<Exception>(() =>
                flightRepository.Object.InsertFlights(filePath, readFromCsv.Object, fileService.Object,
                    flightValidationService.Object));
            
            readFromCsv.VerifyNoOtherCalls();
            fileService.VerifyNoOtherCalls();
            flightValidationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void ReturnAllFlights()
        {
            var fixture = new Fixture();
            var flights = fixture.Create<Flight[]>();
            var flightRepository = new Mock<IFlightRepository>();

            flightRepository
                .Setup(flightRepo => flightRepo.GetAllFlights())
                .Returns(flights);

            var returnedFlights = flightRepository.Object.GetAllFlights();
            
            Assert.Equal(flights.Length, returnedFlights.Length);
            Assert.Equal(flights[0].Name, returnedFlights[0].Name);
        }
        
        [Fact]
        public void ReturnEmptyArrayWhenNoFlightsExist()
        {
            var flightRepository = new Mock<IFlightRepository>();

            flightRepository
                .Setup(flightRepo => flightRepo.GetAllFlights())
                .Returns(Array.Empty<Flight>());

            var returnedFlights = flightRepository.Object.GetAllFlights();
            
            Assert.Empty(returnedFlights);
        }
        
        [Fact]
        public void ReturnFlightByPrice()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const decimal price = 199.99M;

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByPrice(price))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByPrice(price);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasPrice()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const decimal price = 199.99M;

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByPrice(price))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByPrice(price);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByDepartureCountry()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string departureCountry = "USA";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureCountry(departureCountry))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureCountry(departureCountry);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasDepartureCountry()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string departureCountry = "UAE";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureCountry(departureCountry))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureCountry(departureCountry);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByDestinationCountry()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string destinationCountry = "USA";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDestinationCountry(destinationCountry))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByDestinationCountry(destinationCountry);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasDestinationCountry()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string destinationCountry = "UAE";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDestinationCountry(destinationCountry))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByDestinationCountry(destinationCountry);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByDepartureDate()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string departureDate = "7/30/2023";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureDate(departureDate))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureDate(departureDate);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasDepartureDate()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string departureDate = "01/01/2050";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureDate(departureDate))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureDate(departureDate);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByDepartureAirport()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string departureAirport = "Miami International Airport";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureAirport(departureAirport))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureAirport(departureAirport);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasDepartureAirport()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string departureAirport = "Miami International Airport";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByDepartureAirport(departureAirport))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByDepartureAirport(departureAirport);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByArrivalAirport()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string arrivalAirport = "Miami International Airport";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByArrivalAirport(arrivalAirport))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByArrivalAirport(arrivalAirport);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasArrivalAirport()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string arrivalAirport = "Gaza Airport";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByArrivalAirport(arrivalAirport))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByArrivalAirport(arrivalAirport);
            
            Assert.Same(null, returnedFlight);
        }
        
        [Fact]
        public void ReturnFlightByFlightClass()
        {
            var fixture = new Fixture();
            var flightRepository = new Mock<IFlightRepository>();
            var flight = fixture.Create<Flight>();
            const string flightClass = "Economy";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByFlightClass(flightClass))
                .Returns(flight);

            var returnedFlight = flightRepository.Object.SearchFlightsByFlightClass(flightClass);
            
            Assert.Same(flight, returnedFlight);
        }
        
        [Fact]
        public void ReturnNullWhenNoFlightsHasFlightClass()
        {
            var flightRepository = new Mock<IFlightRepository>();
            const string flightClass = "Economy";

            flightRepository
                .Setup(flightRepo => flightRepo.SearchFlightsByFlightClass(flightClass))
                .Returns((Flight)null);

            var returnedFlight = flightRepository.Object.SearchFlightsByFlightClass(flightClass);
            
            Assert.Same(null, returnedFlight);
        }
    }
}