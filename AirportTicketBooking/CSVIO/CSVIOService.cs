using System;
using System.Linq;
using AirportTicketBooking.Flights;
using CsvHelper;

namespace AirportTicketBooking
{
    public class CSVIOService
    {
        public T SearchForRecord<T>(string property, string value, CsvReader csv) 
        {
            var records = csv.GetRecords<T>();

            return records.FirstOrDefault((record) => record.GetType().GetProperty(property)?.GetValue(record).ToString() == value);
        }
        
        public T[] GetAllRecords<T>(CsvReader csv)
        {
            return csv.GetRecords<T>().ToArray();
        }

        public Flight SearchForFlightBy(string category, string value, Flight[] flights)
        {
            return flights.FirstOrDefault(record => category == "DepartureDate"
                ? record.GetType().GetProperty(category)?.GetValue(record).ToString().Split(' ')[0] == value
                : record.GetType().GetProperty(category)?.GetValue(record).ToString() == value);
        }
    }
}
