using System;
using System.Globalization;
using AirportTicketBooking.Bookings;
using AirportTicketBooking.Dtos;
using AirportTicketBooking.Enums;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Managers;
using AirportTicketBooking.Passengers;
using AirportTicketBooking.Services;
using AirportTicketBooking.Utility;

namespace AirportTicketBooking
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var userRole = GetUserInput.GetUserRole();
            
            switch (userRole)
            {
                case UserRoles.Manager:
                {
                    HandleManagerCreation();
                    break;
                }
                case UserRoles.Passenger:
                {
                    HandlePassengerCreation();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user role!");
                    Environment.Exit(1);
                    break;
                }
            }
        }

        private static void HandleManagerCreation()
        {
            #region Create Instances
            
            var csvConfigurations = CSVConfiguration.Instance;
            var csvioService = new CSVIOService();
            
            var flightCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Flight");
            var bookingCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
            var passengerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
            
            var managerRepo = new ManagerRepository();
            var bookingRepo = new BookingRepository(csvioService, bookingCsvReader.CsvReader, flightCsvReader.CsvReader, passengerCsvReader.CsvReader);
            var flightRepo = new FlightRepository(flightCsvReader.CsvReader, csvioService);
            var readFromCsvFile = new ReadFromCsvFile();
            var fileServices = new FileServices();
            var validation = new Validation();

            #endregion

            var doesAccountExist = GetUserInput.GetUserAccountStatus();

            switch (doesAccountExist)
            {
                case DoesAccountExist.Yes:
                {
                    var managerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Manager");
                    
                    var managerName = GetUserInput.GetStringData("name");
                    
                    var manager = managerRepo.SearchForExistingManager(managerName, csvioService, managerCsvReader.CsvReader);
                    
                    managerCsvReader.StreamReader.Close();

                    if (manager is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("managers");
                        Environment.Exit(1);
                    }
                    
                    ConsoleOutput.ShowManagerDetails(manager!.ManagerId, manager.ManagerName, manager.Email);
                    break;
                }
                case DoesAccountExist.No:
                {
                    // Created the csv writer and write to csv file instances here to narrow down the scope and allow the IO to read the file
                    // then write the file without throwing the error.
                    var managerCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Manager");
                    var writeToManagerCsvFile = new WriteToCsvFile(managerCsvWriter);

                    
                    var managerId = GetUserInput.GetIdFromUser();
                    var managerName = GetUserInput.GetStringData("manager name");
                    var managerEmail = GetUserInput.GetStringData("manager email");
                    var managerUsername = GetUserInput.GetStringData("manager username");

                    var managerDto = new ManagerDto(managerId, managerName, managerEmail, managerUsername);

                    var managerFactory = new ManagerService();
                    var manager = managerFactory.CreateNewManager(managerDto);
                    
                    writeToManagerCsvFile.WriteDataToCsv(manager, "Manager");
                    
                    managerCsvWriter.StreamWriter.Close();
                    
                    break;
                }
                default:
                {
                    ConsoleOutput.PrintGeneralMessage("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
            
            var managerOperationChoice = GetUserInput.GetManagerOperation();

            switch (managerOperationChoice)
            {
                case ManagerOperations.Exit:
                {
                    Environment.Exit(0);
                    break;
                }
                case ManagerOperations.ImportFlights:
                {
                    var flightsFilePath = GetUserInput.GetStringData("the path to the flights file");

                    try
                    {
                        flightRepo.InsertFlights(flightsFilePath, readFromCsvFile, fileServices, validation);
                    }
                    catch (Exception e)
                    {
                        if (e.Message.StartsWith("Sharing violation"))
                        {
                            ConsoleOutput.PrintGeneralMessage("You can't insert the same file in the DataStore file!");
                            Environment.Exit(1);
                        }
                        
                        ConsoleOutput.PrintGeneralMessage(e.Message);
                        Environment.Exit(1);
                    }
                    break;
                }
                case ManagerOperations.SearchByFlightId:
                {
                    var flightId = GetUserInput.GetIntegerFromUser("flight id");

                    var bookings = bookingRepo.SearchForBookingsBy("FlightId", flightId.ToString());

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByFlightPrice:
                {
                    var flightPrice = GetUserInput.GetDecimalData("flight price");

                    var bookings = bookingRepo.SearchForBookingsBy("FlightPrice", flightPrice.ToString(CultureInfo.InvariantCulture));

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureCountry:
                {
                    var departureCountry = GetUserInput.GetStringData("departure country");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureCountry", departureCountry);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByDestinationCountry:
                {
                    var destinationCountry = GetUserInput.GetStringData("destination country");

                    var bookings = bookingRepo.SearchForBookingsBy("DestinationCountry", destinationCountry);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureDate:
                {
                    var departureDate = GetUserInput.GetDateOnlyData("departure date");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureDate", departureDate);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureAirport:
                {
                    var departureAirport = GetUserInput.GetStringData("departure airport");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureAirport", departureAirport);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByArrivalAirport:
                {
                    var arrivalAirport = GetUserInput.GetStringData("arrival airport");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureAirport", arrivalAirport);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByFlightClass:
                {
                    var flightClass = GetUserInput.GetFlightClassCategory();

                    var bookings = bookingRepo.SearchForBookingsBy("FlightClass", flightClass);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(booking.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }

                    break;
                }
                case ManagerOperations.SearchByPassengerId:
                {
                    var passengerId = GetUserInput.GetIntegerFromUser("passenger id");

                    var bookings = bookingRepo.SearchForBookingByPassenger(passengerId);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no bookings for this passenger id value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid choice value, please choose on of the options numbers.");
                    Environment.Exit(1);
                    break;
                }
            }
        }

        private static void HandlePassengerCreation()
        {

            #region Create Instances

            var csvConfigurations = CSVConfiguration.Instance;
            var csvioService = new CSVIOService();

            var passengerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
            var flightCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Flight");
            var bookingCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
            
            var passengerRepo = new PassengerRepository();
            var bookingRepo = new BookingRepository(csvioService, bookingCsvReader.CsvReader, flightCsvReader.CsvReader, passengerCsvReader.CsvReader);
            var flightRepo = new FlightRepository(flightCsvReader.CsvReader, csvioService);
            var bookingService = new BookingService();
            var booking = new Booking();

            #endregion
            
            var doesAccountExist = GetUserInput.GetUserAccountStatus();

            switch (doesAccountExist)
            {
                case DoesAccountExist.Yes:
                {
                    var passengerName = GetUserInput.GetStringData("passenger name");

                    var passenger = passengerRepo.SearchForPassenger(passengerName, csvioService, passengerCsvReader.CsvReader);

                    if (passenger is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("passengers");
                        Environment.Exit(1);
                    }
                    
                    ConsoleOutput.ShowPassengerDetails(passenger!.PassengerId, passenger.PassengerName, passenger.Email);
                    
                    break;
                }
                case DoesAccountExist.No:
                {
                    var passengerCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
                    var writeToPassengerCsvFile = new WriteToCsvFile(passengerCsvWriter);

                    var passengerFactory = new PassengerService();

                    var passengerId = GetUserInput.GetIdFromUser();
                    var passengerName = GetUserInput.GetStringData("passenger name");
                    var passengerEmail = GetUserInput.GetStringData("passenger email");
                    var passengerPassportNumber = GetUserInput.GetPassportNumber();
                    var passengerCreditCardNumber = GetUserInput.GetCreditCardNumber();

                    var passengerDto = new PassengerDto(
                        passengerId,
                        passengerName,
                        passengerEmail,
                        passengerPassportNumber,
                        passengerCreditCardNumber
                    );
                    
                    var passenger = passengerFactory.CreateNewPassenger(passengerDto);
                    
                    writeToPassengerCsvFile.WriteDataToCsv(passenger, "Passenger");
                    
                    passengerCsvWriter.StreamWriter.Close();
                    
                    break;
                }
                default:
                {
                    ConsoleOutput.PrintGeneralMessage("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }

            var passengerOperation = GetUserInput.GetPassengerOperations();

            switch (passengerOperation)
            {
                case PassengerOperations.GetFlights:
                {
                    var flights = flightRepo.GetAllFlights();

                    if (flights.Length > 0)
                    {
                        foreach (var flight in flights)
                        {
                            ConsoleOutput.PrintGeneralMessage(flight.ToString());
                        } 
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }
                    
                    break;
                }
                case PassengerOperations.SearchByFlightPrice:
                {
                    var flightPrice = GetUserInput.GetDecimalData("flight price");
                    
                    var flight = flightRepo.SearchFlightsByPrice(flightPrice);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureCountry:
                {
                    var departureCountry = GetUserInput.GetStringData("departure country");
                    
                    var flight = flightRepo.SearchFlightsByDepartureCountry(departureCountry);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDestinationCountry:
                {
                    var destinationCountry = GetUserInput.GetStringData("destination country");
                    
                    var flight = flightRepo.SearchFlightsByDestinationCountry(destinationCountry);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureDate:
                {
                    var departureDate = GetUserInput.GetDateOnlyData("departure date");
                    
                    var flight = flightRepo.SearchFlightsByDepartureDate(departureDate);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureAirport:
                {
                    var departureAirport = GetUserInput.GetStringData("departure airport");
                    
                    var flight = flightRepo.SearchFlightsByDepartureAirport(departureAirport);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByArrivalAirport:
                {
                    var departureAirport = GetUserInput.GetStringData("departure airport");
                    
                    var flight = flightRepo.SearchFlightsByArrivalAirport(departureAirport);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByFlightClass:
                {
                    var flightClass = GetUserInput.GetStringData("flight class");
                    
                    var flight = flightRepo.SearchFlightsByFlightClass(flightClass);

                    if (flight is null)
                    {
                        ConsoleOutput.NoDataFoundMessage("flights");
                        Environment.Exit(1);
                    }

                    ConsoleOutput.PrintGeneralMessage(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.MakeABooking:
                {
                    var bookingCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var writeToBookingCsvFile = new WriteToCsvFile(bookingCsvWriter);
                    
                    var bookingId = GetUserInput.GetIdFromUser();
                    var flightId = GetUserInput.GetIntegerFromUser("flight id");
                    var passengerId = GetUserInput.GetIntegerFromUser("passenger id");
                    var seatsNumber = GetUserInput.GetIntegerFromUser("seats number");

                    var bookingData = new BookingDto(
                        bookingId,
                        flightId,
                        passengerId,
                        seatsNumber,
                        DateTime.Now
                    );

                    var newBooking = bookingService.CreateNewBooking(bookingData);
                    
                    writeToBookingCsvFile.WriteDataToCsv(newBooking, "Booking");
                    
                    Console.WriteLine(newBooking.ToString());
                    break;
                }
                case PassengerOperations.CancelABooking:
                {
                    var bookingId = GetUserInput.GetIntegerFromUser("booking id");
                    
                    var editedBooking = booking.EditBooking(bookingId, csvioService, bookingCsvReader.CsvReader);
                    
                    ConsoleOutput.PrintGeneralMessage($"The booking with the id of {editedBooking.BookingId} got edited successfully!");
                    break;
                }
                case PassengerOperations.ModifyABooking:
                {
                    var bookingCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var writeToBookingCsvFile = new WriteToCsvFile(bookingCsvWriter);
                    
                    var bookingId = GetUserInput.GetIntegerFromUser("booking id");
                    
                    booking.CancelBooking(bookingId, csvioService, bookingCsvReader.CsvReader, writeToBookingCsvFile);
                    
                    ConsoleOutput.PrintGeneralMessage($"Booking with {bookingId} got deleted successfully!");
                    break;
                }
                case PassengerOperations.GetBookings:
                {
                    var bookings = bookingRepo.GetBookings();

                    if (bookings.Length > 0)
                    {
                        foreach (var bookingItem in bookings)
                        {
                            ConsoleOutput.PrintGeneralMessage(bookingItem.ToString());
                        }
                    }
                    else
                    {
                        ConsoleOutput.NoDataFoundMessage("bookings");
                        Environment.Exit(1);
                    }
                    
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
        }
    }
}