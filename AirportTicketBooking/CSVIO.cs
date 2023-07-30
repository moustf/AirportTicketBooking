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
        private readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

        public Manager SearchForManager(string name) 
        {
            using var reader = new StreamReader($@"{_currentDir}/DataStore/Manager.csv");
            using var csv = new CsvReader(reader, _csvConfiguration);
            var records = csv.GetRecords<Manager>();

            return records.SingleOrDefault((record) => record.ManagerName == name);
        }

        public IEnumerable<Manager> GetAllManagers()
        {
            using var reader = new StreamReader($@"{_currentDir}/DataStore/Manager.csv");
            using var csv = new CsvReader(reader, _csvConfiguration);
            return csv.GetRecords<Manager>();
        }

        public void WriteDataToCsv(IEnumerable<object> objects, string fileName)
        {
            var enumerableObjects = objects as object[] ?? objects.ToArray();
            
            foreach (var obj in enumerableObjects)
            {
                var objType = obj.GetType();
                if (objType != typeof(Passenger) || objType != typeof(Manager) || objType != typeof(Flight) ||
                    objType != typeof(Booking))
                {
                    throw new Exception("The objects types are not consistent!");
                }
            }
            
            using var stream = File.Open($@"{_currentDir}/DataStore/{fileName}.csv", FileMode.Append);
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, _csvConfiguration);
            
            csv.WriteRecords(enumerableObjects);
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
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            
            csv.WriteRecord(obj);
            csv.NextRecord();
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
            File.WriteAllLines($"{_currentDir}/DateStore/Flight.csv", lines);
        }

        public List<string> ValidateFlights(string pathToFile)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
                var errors = new List<string>();
                
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                
                    if (!int.TryParse(values[0], out var res))
                    {
                        errors.Add("The first column must be the flight id of type integer!");
                    }
                    
                    if (values[1].Split(':').Length <= 0)
                    {
                        errors.Add("The name of the flight must implement this format: 'FlightId:DepartureCountry:ArrivalCountry:FlightClass'.");
                    }

                    if (values[2] != "economy" && values[2] != "business" && values[2] != "first class")
                    {
                        errors.Add("The flight class can either be 'economy', 'business', or 'first class'.");
                    }

                    if (decimal.TryParse(values[3], out var dRes))
                    {
                        errors.Add("The money must be decimal type, with floating point numbers.");
                    }

                    if (string.IsNullOrWhiteSpace(values[4]))
                    {
                        errors.Add("Departure country can't be null!");
                    }

                    if (string.IsNullOrWhiteSpace(values[5]))
                    {
                        errors.Add("Arrival country can't be null!");
                    }

                    if (string.IsNullOrWhiteSpace(values[6]))
                    {
                        errors.Add("Departure airport can't be null!");
                    }

                    if (string.IsNullOrWhiteSpace(values[7]))
                    {
                        errors.Add("Arrival airport can't be null!");
                    }

                    if (string.IsNullOrWhiteSpace(values[8]))
                    {
                        errors.Add("Airlines name can't be null!");
                    }

                    if (DateTime.TryParse(values[9], out var dtRes))
                    {
                        errors.Add("Departure time must be a valid time stamp following this format 'mm/dd/yyyy hh:mm:ss'.");
                    }
                    
                    errors.Add($"The following error happende in the row with the id number: {values[0]}");
                }
                return errors;
        }
    }
}


