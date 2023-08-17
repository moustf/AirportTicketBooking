using System;
using System.Linq;
using AirportTicketBooking.Services;

namespace AirportTicketBooking
{
    public class ReadFromCsvFile
    {
        public void RegisterFlightsData(string filePath, FileServices fileServices, FlightValidationService flightValidationService)
        {
            var fileLines = fileServices.ReadCsvFile(filePath);
                var csvValidationErrors = flightValidationService.ValidateFlights(fileLines);

                if (csvValidationErrors.Any())
                {
                    foreach (var error in csvValidationErrors)
                    {
                        Console.WriteLine(error);
                        Environment.Exit(1);
                    } 
                    return;
                }

                fileServices.WriteToCsvFile(filePath, fileLines);
            // $@"{_csvConfigurationObject.CurrentDirectory}/DataStore/Flight.csv"
        }
    }
}