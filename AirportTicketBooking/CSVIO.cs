using System;
using System.Collections.Generic;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace AirportTicketBooking
{
    public class CSVIO
    {
        public CSVIO()
        {
            var fullHomeDir = Directory.GetCurrentDirectory().Split('/').TakeWhile(dir => dir != "bin");
            _currentDir = String.Join("/", fullHomeDir);
        }
        private readonly string _currentDir;
        private readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };

        public T SearchForRecord<T>(string prop, string value, string fileName) 
        {
            using var reader = new StreamReader($@"{_currentDir}/DataStore/{fileName}.csv");
            using var csv = new CsvReader(reader, _csvConfiguration);
            var records = csv.GetRecords<T>();

            return records.FirstOrDefault((record) => record.GetType().GetProperty(prop)?.GetValue(record).ToString() == value);
        }
        
        public T[] GetAllRecords<T>(string fileName)
        {
            using var reader = new StreamReader($@"{_currentDir}/DataStore/{fileName}.csv");
            using var csv = new CsvReader(reader, _csvConfiguration);
            return csv.GetRecords<T>().ToArray();
        }

        public void RemoveBooking(int bookingId)
        {
            var bookings = GetAllRecords<Booking>("Booking");

            var bookingsToWrite = bookings.Select(booking => booking.BookingId != bookingId);
            
            WriteDataToCsv(bookingsToWrite, "Booking");
        }

        public void WriteDataToCsv(object obj, string fileName)
        {
            var objType = obj.GetType();
            if (
                objType.Name != nameof(Passenger)
                && objType.Name != nameof(Manager)
                && objType.Name != nameof(Flight)
                && objType.Name != nameof(Booking)
                )
            {
                throw new Exception("The objects types are not consistent!");
            }
            
            using var stream = File.Open($@"{_currentDir}/DataStore/{fileName}.csv", FileMode.Append);
            using var writer = new StreamWriter(stream, Encoding.UTF8);
            using var csv = new CsvWriter(writer, _csvConfiguration);
            
            csv.WriteRecord(obj);
            csv.NextRecord();
        }
        
        public Flight SearchForFlightBy(string category, string value)
        {
            var records = GetAllRecords<Flight>("Flight");

            return records.FirstOrDefault(record => category == "DepartureDate"
                ? record.GetType().GetProperty(category)?.GetValue(record).ToString().Split(' ')[0] == value
                : record.GetType().GetProperty(category)?.GetValue(record).ToString() == value);
        }

        public void ReadFlightsData(string filePath)
        {
            if (!filePath.Contains(".csv"))
            {
                throw new Exception("The file type is ont supported!");
            }
        
            if (File.Exists(filePath))
            {
                var errors = ValidateFlights(filePath);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                        Environment.Exit(1);
                        return;
                    }
                }
            }

            var lines = File.ReadAllLines(filePath);
            File.WriteAllLines($"{_currentDir}/DataStore/Flight.csv", lines);
        }

        public Flight[] GetAllFlightsData()
        {
            return GetAllRecords<Flight>("Flight");
        }

        private List<string> ValidateFlights(string pathToFile)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            var errors = new List<string>();

            foreach (var line in lines)
            {
                var values = line.Split(',');

                if (!int.TryParse(values[0], out var res))
                {
                    errors.Add($"{values[0]}: The first column must be the flight id of type integer!");
                }

                if (values[1].Split(':').Length <= 0)
                {
                    errors.Add(
                        $"{values[0]}: The name of the flight must implement this format: 'FlightId:DepartureCountry:ArrivalCountry:FlightClass'.");
                }

                if (values[2] != "economy" && values[2] != "business" && values[2] != "first class")
                {
                    errors.Add($"{values[0]}: The flight class can either be 'economy', 'business', or 'first class'.");
                }

                if (!decimal.TryParse(values[3], out var dRes))
                {
                    errors.Add($"{values[0]}: The money must be decimal type, with floating point numbers.");
                }

                if (string.IsNullOrWhiteSpace(values[4]))
                {
                    errors.Add($"{values[0]}: Departure country can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[5]))
                {
                    errors.Add($"{values[0]}: Arrival country can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[6]))
                {
                    errors.Add($"{values[0]}: Departure airport can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[7]))
                {
                    errors.Add($"{values[0]}: Arrival airport can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[8]))
                {
                    errors.Add($"{values[0]}: Airlines name can't be null!");
                }

                if (!DateTime.TryParse(values[9], out var dtRes))
                {
                    errors.Add(
                        $"{values[0]}: Departure time must be a valid time stamp following this format 'mm/dd/yyyy hh:mm:ss'.");
                }
            }

            return errors;
        }
    }
}
