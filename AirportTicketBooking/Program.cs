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
            var userRole = UserInputUtility.GetUserRole();
            
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
            var bookingRepo = new BookingRepository(csvioService, bookingCsvReader, flightCsvReader, passengerCsvReader);
            var flightRepo = new FlightRepository(flightCsvReader, csvioService);
            var readFromCsvFile = new ReadFromCsvFile();
            var fileServices = new FileServices();
            var validation = new FlightValidationService();

            #endregion

            var doesAccountExist = UserInputUtility.GetUserAccountStatus();

            switch (doesAccountExist)
            {
                case DoesAccountExist.Yes:
                {
                    var managerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Manager");
                    
                    var managerName = UserInputUtility.GetStringData("name");
                    
                    var manager = managerRepo.SearchForExistingManager(managerName, csvioService, managerCsvReader);

                    if (manager is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("managers"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandleManagerCreation();
                    }
                    
                    Console.WriteLine(ConsoleOutputUtility.ShowManagerDetails(manager!.Id, manager.Name, manager.Email));
                    break;
                }
                case DoesAccountExist.No:
                {
                    // Created the csv writer and write to csv file instances here to narrow down the scope and allow the IO to read the file
                    // then write the file without throwing the error.
                    var managerCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Manager");
                    var writeToManagerCsvFile = new WriteToCsvFile(managerCsvWriter);

                    
                    var managerId = UserInputUtility.GetIdFromUser();
                    var managerName = UserInputUtility.GetStringData("manager name");
                    var managerEmail = UserInputUtility.GetStringData("manager email");
                    var managerUsername = UserInputUtility.GetStringData("manager username");

                    var managerDto = new ManagerDto(managerId, managerName, managerEmail, managerUsername);

                    var managerFactory = new ManagerService();
                    var manager = managerFactory.CreateNewManager(managerDto);
                    
                    writeToManagerCsvFile.WriteDataToCsv(manager, "Manager");

                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
            
            var managerOperationChoice = UserInputUtility.GetManagerOperation();

            switch (managerOperationChoice)
            {
                case ManagerOperations.Exit:
                {
                    Environment.Exit(0);
                    break;
                }
                case ManagerOperations.ImportFlights:
                {
                    var flightsFilePath = UserInputUtility.GetStringData("the path to the flights file");

                    try
                    {
                        flightRepo.InsertFlights(flightsFilePath, readFromCsvFile, fileServices, validation);
                    }
                    catch (Exception e)
                    {
                        if (e.Message.StartsWith("Sharing violation"))
                        {
                            Console.WriteLine("You can't insert the same file in the DataStore file!");
                            Console.WriteLine("You will be redirected to the start of the program.");
                            HandleManagerCreation();
                        }
                        
                        Console.WriteLine(e.Message);
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandleManagerCreation();
                    }
                    break;
                }
                case ManagerOperations.SearchByFlightId:
                {
                    var flightId = UserInputUtility.GetIntegerFromUser("flight id");

                    var bookings = bookingRepo.SearchForBookingsBy("FlightId", flightId.ToString());

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByFlightPrice:
                {
                    var flightPrice = UserInputUtility.GetDecimalData("flight price");

                    var bookings = bookingRepo.SearchForBookingsBy("FlightPrice", flightPrice.ToString(CultureInfo.InvariantCulture));

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureCountry:
                {
                    var departureCountry = UserInputUtility.GetStringData("departure country");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureCountry", departureCountry);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByDestinationCountry:
                {
                    var destinationCountry = UserInputUtility.GetStringData("destination country");

                    var bookings = bookingRepo.SearchForBookingsBy("DestinationCountry", destinationCountry);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureDate:
                {
                    var departureDate = UserInputUtility.GetDateOnlyData("departure date");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureDate", departureDate);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByDepartureAirport:
                {
                    var departureAirport = UserInputUtility.GetStringData("departure airport");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureAirport", departureAirport);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByArrivalAirport:
                {
                    var arrivalAirport = UserInputUtility.GetStringData("arrival airport");

                    var bookings = bookingRepo.SearchForBookingsBy("DepartureAirport", arrivalAirport);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByFlightClass:
                {
                    var flightClass = UserInputUtility.GetFlightClassCategory();

                    var bookings = bookingRepo.SearchForBookingsBy("FlightClass", flightClass);

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        HandleManagerCreation();
                    }

                    break;
                }
                case ManagerOperations.SearchByPassengerId:
                {
                    var passengerId = UserInputUtility.GetIntegerFromUser("passenger id");

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
                        HandleManagerCreation();
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

            #region Create Instances One

            var csvConfigurations = CSVConfiguration.Instance;
            var csvioService = new CSVIOService();

            var flightCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Flight");
            
            var passengerRepo = new PassengerRepository();
            var flightRepo = new FlightRepository(flightCsvReader, csvioService);
            var bookingService = new BookingService();
            var booking = new Booking();

            #endregion
            
            var doesAccountExist = UserInputUtility.GetUserAccountStatus();

            switch (doesAccountExist)
            {
                case DoesAccountExist.Yes:
                {
                    var passengerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
                    
                    var passengerName = UserInputUtility.GetStringData("passenger name");

                    var passenger = passengerRepo.SearchForPassenger(passengerName, csvioService, passengerCsvReader.CsvReader);

                    if (passenger is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("passengers"));
                        HandlePassengerCreation();
                    }
                    
                    Console.WriteLine(ConsoleOutputUtility.ShowPassengerDetails(passenger!.Id, passenger.Name, passenger.Email));
                    
                    break;
                }
                case DoesAccountExist.No:
                {
                    var passengerCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
                    var writeToPassengerCsvFile = new WriteToCsvFile(passengerCsvWriter);

                    var passengerFactory = new PassengerService();

                    var passengerId = UserInputUtility.GetIdFromUser();
                    var passengerName = UserInputUtility.GetStringData("passenger name");
                    var passengerEmail = UserInputUtility.GetStringData("passenger email");
                    var passengerPassportNumber = UserInputUtility.GetPassportNumber();
                    var passengerCreditCardNumber = UserInputUtility.GetCreditCardNumber();

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
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }

            var passengerOperation = UserInputUtility.GetPassengerOperations();

            switch (passengerOperation)
            {
                case PassengerOperations.Exit:
                {
                    Environment.Exit(0);
                    break;
                }
                case PassengerOperations.GetFlights:
                {
                    var flights = flightRepo.GetAllFlights();

                    if (flights.Length > 0)
                    {
                        foreach (var flight in flights)
                        {
                            Console.WriteLine(flight.ToString());
                        } 
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }
                    
                    break;
                }
                case PassengerOperations.SearchByFlightPrice:
                {
                    var flightPrice = UserInputUtility.GetDecimalData("flight price");
                    
                    var flight = flightRepo.SearchFlightsByPrice(flightPrice);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureCountry:
                {
                    var departureCountry = UserInputUtility.GetStringData("departure country");
                    
                    var flight = flightRepo.SearchFlightsByDepartureCountry(departureCountry);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDestinationCountry:
                {
                    var destinationCountry = UserInputUtility.GetStringData("destination country");
                    
                    var flight = flightRepo.SearchFlightsByDestinationCountry(destinationCountry);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureDate:
                {
                    var departureDate = UserInputUtility.GetDateOnlyData("departure date");
                    
                    var flight = flightRepo.SearchFlightsByDepartureDate(departureDate);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByDepartureAirport:
                {
                    var departureAirport = UserInputUtility.GetStringData("departure airport");
                    
                    var flight = flightRepo.SearchFlightsByDepartureAirport(departureAirport);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByArrivalAirport:
                {
                    var departureAirport = UserInputUtility.GetStringData("departure airport");
                    
                    var flight = flightRepo.SearchFlightsByArrivalAirport(departureAirport);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.SearchByFlightClass:
                {
                    var flightClass = UserInputUtility.GetStringData("flight class");
                    
                    var flight = flightRepo.SearchFlightsByFlightClass(flightClass);

                    if (flight is null)
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("flights"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
                    }

                    Console.WriteLine(flight!.ToString());
                    
                    break;
                }
                case PassengerOperations.MakeABooking:
                {
                    var bookingCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var writeToBookingCsvFile = new WriteToCsvFile(bookingCsvWriter);
                    
                    var bookingId = UserInputUtility.GetIdFromUser();
                    var flightId = UserInputUtility.GetIntegerFromUser("flight id");
                    var passengerId = UserInputUtility.GetIntegerFromUser("passenger id");
                    var seatsNumber = UserInputUtility.GetIntegerFromUser("seats number");

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
                    var bookingCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");

                    var bookingId = UserInputUtility.GetIntegerFromUser("booking id");
                    
                    var editedBooking = booking.EditBooking(bookingId, csvioService, bookingCsvReader.CsvReader);
                    
                    Console.WriteLine($"The booking with the id of {editedBooking.BookingId} got edited successfully!");
                    break;
                }
                case PassengerOperations.ModifyABooking:
                {
                    var bookingCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var bookingCsvWriter = new CSVWriterService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var writeToBookingCsvFile = new WriteToCsvFile(bookingCsvWriter);
                    
                    var bookingId = UserInputUtility.GetIntegerFromUser("booking id");
                    
                    booking.CancelBooking(bookingId, csvioService, bookingCsvReader, writeToBookingCsvFile);
                    
                    Console.WriteLine($"Booking with {bookingId} got deleted successfully!");
                    break;
                }
                case PassengerOperations.GetBookings:
                {
                    var bookingCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Booking");
                    var passengerCsvReader = new CSVReaderService(csvConfigurations.CurrentDirectory, csvConfigurations.CsvConfiguration, "Passenger");
                    var bookingRepo = new BookingRepository(csvioService, bookingCsvReader, flightCsvReader, passengerCsvReader);

                    var bookings = bookingRepo.GetBookings();

                    if (bookings.Length > 0)
                    {
                        foreach (var bookingItem in bookings)
                        {
                            Console.WriteLine(bookingItem.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConsoleOutputUtility.NoDataFoundMessage("bookings"));
                        Console.WriteLine("You will be redirected to the start of the program.");
                        HandlePassengerCreation();
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