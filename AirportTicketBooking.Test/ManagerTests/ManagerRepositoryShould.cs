using AirportTicketBooking.Comparers;
using AirportTicketBooking.Managers;
using AirportTicketBooking.Passengers;
using Xunit;
using Moq;

namespace AirportTicketBooking.Test.ManagerTests
{
    public class ManagerRepositoryShould
    {
            [Fact]
            public void ReturnExistingManager()
            {
                var csvioService = new Mock<ICSVIOService>();
                var csvioReader = new Mock<CSVReaderService>();
                var managerRepository = new Mock<IManagerRepository>();
                var manager = new Mock<Manager>();

                managerRepository.Setup(
                    managerRepo =>
                        managerRepo.SearchForManager(It.IsAny<string>(), csvioService.Object,
                        csvioReader.Object
                    ))
                    .Returns(manager.Object);

                var expected =
                    managerRepository.Object.SearchForManager(It.IsAny<string>(), csvioService.Object, csvioReader.Object);
            
                Assert.Equal(manager.Object, expected, new ManagerComparer());
            }
            [Fact]
            public void ReturnNullWhenPassingNonExistingManagerName()
            {
                var csvioService = new Mock<ICSVIOService>();
                var csvioReader = new Mock<CSVReaderService>();
                var managerRepository = new Mock<IManagerRepository>();
            
                managerRepository.Setup(
                        managerRepo =>
                            managerRepo.SearchForManager(It.IsAny<string>(), csvioService.Object,
                                csvioReader.Object
                            ))
                    .Returns((Manager)null);

                var expected =
                    managerRepository.Object.SearchForManager(It.IsAny<string>(), csvioService.Object, csvioReader.Object);
            
                Assert.Equal(null, expected);
            }
    }
}