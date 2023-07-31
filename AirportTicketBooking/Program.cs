using System;
using System.Globalization;

namespace AirportTicketBooking
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Ticket Booking System!");
            Console.WriteLine("What is you role? 1 for Manager, and 2 for Passenger.");
            var roleTest = Console.ReadLine();
            int role;
            
            while (!int.TryParse(roleTest, out role))
            {
                Console.WriteLine("Please specify a valid number!");
                roleTest = Console.ReadLine();
            }
            
            switch (role)
            {
                case 1:
                {
                    HandleManagerCreation();
                    break;
                }
                case 2:
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

        public static void HandleManagerCreation()
        {
            Console.WriteLine("Do you have an account? 1 for having an account, and 2 for new manager users.");
            var choiceTest = Console.ReadLine();
            int choice;

            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            switch (choice)
            {
                case 1:
                {
                    var managerRepo = new ManagerRepository();
                    // Call the csv parsing method and get the user data from there.
                    Console.WriteLine("Please specify your name.");
                    var managerName = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(managerName))
                    {
                        Console.WriteLine("Please enter a valid name.");
                        managerName = Console.ReadLine();
                    }

                    try
                    {
                        var manager = managerRepo.SearchForExistingManager(managerName);
                        Console.WriteLine($"{manager.ManagerId}: {manager.ManagerName} with the email {manager.Email}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Environment.Exit(1);
                    }
                    break;
                }
                case 2:
                {
                    // Create new manager object and add it to the managers file.
                    var managerFactory = new ManagerFactory();
                    managerFactory.CreateNewManager();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
            
            Console.WriteLine("Do you want to do other operations?");
            Console.WriteLine("1 --> Import flights data from a CSV file.");
            Console.WriteLine("2 --> Search for a booking by the flight id.");
            Console.WriteLine("3 --> Search for a booking by the flight price.");
            Console.WriteLine("4 --> Search for a booking by the departure country.");
            Console.WriteLine("5 --> Search for a booking by the destination country.");
            Console.WriteLine("6 --> Search for a booking by the departure date.");
            Console.WriteLine("7 --> Search for a booking by the departure airport.");
            Console.WriteLine("8 --> Search for a booking by the arrival airport.");
            Console.WriteLine("9 --> Search for a booking by the flight class.");
            Console.WriteLine("10 --> Search for a booking by the passenger id.");
            Console.WriteLine("0 --> Exit.");
            
            choiceTest = Console.ReadLine();
            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            var bookingRepo = new BookingRepository();

            switch (choice)
            {
                case 0:
                {
                    Environment.Exit(0);
                    break;
                }
                case 1:
                {
                    var flightRepo = new FlightRepository();
                    
                    Console.WriteLine("Please provide the path to the flights file.");
                    var path = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(path))
                    {
                        Console.WriteLine("Please provide a valid path to the flights file.");
                        path = Console.ReadLine();
                    }
                    
                    flightRepo.InsertFlights(path);
                    break;
                }
                case 2:
                {
                    Console.WriteLine("What is the id of the flights you want to search with?");
                    var flightIdTest = Console.ReadLine();
                    int flightId;

                    while (!int.TryParse(flightIdTest, out flightId))
                    {
                        Console.WriteLine("Please specify a valid number!");
                    }

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
                        Console.WriteLine("There is no bookings for this flight id.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 3:
                {
                    Console.WriteLine("What is the price of the flights you want to search with?");
                    var flightPriceTest = Console.ReadLine();
                    decimal flightPrice;

                    while (!decimal.TryParse(flightPriceTest, out flightPrice))
                    {
                        Console.WriteLine("Please specify a valid price!");
                    }

                    var bookings = bookingRepo.SearchForBookingsBy(
                        "FlightPrice", flightPrice.ToString(CultureInfo.InvariantCulture)
                        );

                    if (bookings.Length > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no bookings for this flight price.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 4:
                {
                    Console.WriteLine("What is the departure country of the flights you want to search with?");
                    var departureCountry = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(departureCountry))
                    {
                        Console.WriteLine("Please specify a valid departure country!");
                        departureCountry = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this departure country value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 5:
                {
                    Console.WriteLine("What is the destination country of the flights you want to search with?");
                    var destinationCountry = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(destinationCountry))
                    {
                        Console.WriteLine("Please specify a valid destination country!");
                        destinationCountry = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this destination country value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 6:
                {
                    Console.WriteLine("What is the departure date of the flights you want to search with?");
                    Console.WriteLine("Please specify the data with the format of mm/dd/yyyy");
                    var departureDate = Console.ReadLine();

                    while (departureDate?.Split('/').Length < 0 || string.IsNullOrWhiteSpace(departureDate))
                    {
                        Console.WriteLine("Please specify a valid departure date!");
                        departureDate = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this departure date value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 7:
                {
                    Console.WriteLine("What is the departure airport of the flights you want to search with?");
                    var departureAirport = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(departureAirport))
                    {
                        Console.WriteLine("Please specify a valid departure airport!");
                        departureAirport = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this departure airport value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 8:
                {
                    Console.WriteLine("What is the arrival airport of the flights you want to search with?");
                    var arrivalAirport = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(arrivalAirport))
                    {
                        Console.WriteLine("Please specify a valid arrival airport!");
                        arrivalAirport = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this arrival airport value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 9:
                {
                    Console.WriteLine("What is the flight class of the flights you want to search with?");
                    Console.WriteLine("Flight classes can either be 'economy', 'business', or 'first class'.");
                    var flightClass = Console.ReadLine();

                    while (flightClass != "economy" || flightClass != "business" ||  flightClass != "first class")
                    {
                        Console.WriteLine("Please specify a valid flight class!");
                        flightClass = Console.ReadLine();
                    }

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
                        Console.WriteLine("There is no bookings for this flight class value.");
                        Environment.Exit(1);
                    }

                    break;
                }
                case 10:
                {
                    Console.WriteLine("What is the id of the passenger you want to search with?");
                    var passengerIdTest = Console.ReadLine();
                    int passengerId;

                    while (!int.TryParse(passengerIdTest, out passengerId))
                    {
                        Console.WriteLine("Please specify a valid number for the id!");
                        passengerIdTest = Console.ReadLine();
                    }

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

        public static void HandlePassengerCreation()
        {
            Console.WriteLine("Do you have an account? 1 for having an account, and 2 for new passenger users.");
            var choiceTest = Console.ReadLine();
            int choice;

            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            switch (choice)
            {
                case 1:
                {
                    var passengerRepo = new PassengerRepository();
                    // Call the csv parsing method and get the user data from there.
                    Console.WriteLine("Please specify your name.");
                    var passengerName = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(passengerName))
                    {
                        Console.WriteLine("Please enter a valid name.");
                        passengerName = Console.ReadLine();
                    }

                    try
                    {
                        var passenger = passengerRepo.SearchForPassenger(passengerName);
                        Console.WriteLine($"{passenger.PassengerId}: {passenger.PassengerName} with the email {passenger.Email}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Environment.Exit(1);
                    }
                    break;
                }
                case 2:
                {
                    // Create new manager object and add it to the managers file.
                    var passengerFactory = new PassengerFactory();
                    passengerFactory.CreateNewPassenger();
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid value for the user choice!");
                    Environment.Exit(1);
                    break;
                }
            }
            
            Console.WriteLine("Do you want to do other operations?");
            Console.WriteLine("Please note that in order to book a flight, you need to remember it's flight id and passenger id(your id).");
            Console.WriteLine("1 --> Get all the flights.");
            Console.WriteLine("2 --> Search for a flight by the price.");
            Console.WriteLine("3 --> Search for a flight by the departure country.");
            Console.WriteLine("4 --> Search for a flight by the destination country.");
            Console.WriteLine("5 --> Search for a flight by the departure date.");
            Console.WriteLine("6 --> Search for a flight by the departure airport.");
            Console.WriteLine("7 --> Search for a flight by the arrival airport.");
            Console.WriteLine("8 --> Search for a flight by the flight class.");
            Console.WriteLine("9 --> Make a booking.");
            Console.WriteLine("10 --> Cancel a booking.");
            Console.WriteLine("11 --> Modify a booking.");
            Console.WriteLine("12 --> Get all bookings.");
            Console.WriteLine("0 --> Exit.");
            
            choiceTest = Console.ReadLine();
            while (!int.TryParse(choiceTest, out choice))
            {
                Console.WriteLine("Please specify a valid number!");
            }

            var flightRepo = new FlightRepository();
            var booking = new Booking();
            var bookingFactory = new BookingFactory();

            switch (choice)
            {
                case 1:
                {
                    var flights = flightRepo.GetAllFlights();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight.ToString());
                    }
                    break;
                }
                case 2:
                {
                    var flight = flightRepo.SearchFlightsByPrice();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 3:
                {
                    var flight = flightRepo.SearchFlightsByDepartureCountry();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 4:
                {
                    var flight = flightRepo.SearchFlightsByDestinationCountry();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 5:
                {
                    var flight = flightRepo.SearchFlightsByDepartureDate();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 6:
                {
                    var flight = flightRepo.SearchFlightsByDepartureAirport();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 7:
                {
                    var flight = flightRepo.SearchFlightsByArrivalAirport();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 8:
                {
                    var flight = flightRepo.SearchFlightsByFlightClass();
                    Console.WriteLine(flight.ToString());
                    break;
                }
                case 9:
                {
                    var bookingIns = bookingFactory.CreateNewBooking();
                    Console.WriteLine(bookingIns.ToString());
                    break;
                }
                case 10:
                {
                    Console.WriteLine("Please enter the id of the booking you want to edit.");
                    var bookingIdTest = Console.ReadLine();
                    int bookingId;
                    while (!int.TryParse(bookingIdTest, out bookingId))
                    {
                        Console.WriteLine("Please enter a valid integer id!");
                        bookingIdTest = Console.ReadLine();
                    }
                    
                    var bookingIns = booking.EditBooking(bookingId);
                    Console.WriteLine($"The booking with the id of {bookingIns.BookingId} got edited successfully!");
                    break;
                }
                case 11:
                {
                    Console.WriteLine("Please enter the id of the booking you want to cancel.");
                    var bookingIdTest = Console.ReadLine();
                    int bookingId;
                    while (!int.TryParse(bookingIdTest, out bookingId))
                    {
                        Console.WriteLine("Please enter a valid integer id!");
                        bookingIdTest = Console.ReadLine();
                    }
                    
                    booking.CancelBooking(bookingId);
                    
                    Console.WriteLine($"Booking with {bookingId} got deleted successffully!");
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