using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AirportTicketBooking
{
    public class CSVConfiguration
    {
        public CSVConfiguration()
        {
            var fullHomeDir = Directory.GetCurrentDirectory().Split('/').TakeWhile(dir => dir != "bin");
            CurrentDirectory = String.Join("/", fullHomeDir);
            
            CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
        }
        
        public string CurrentDirectory { get; }
        public CsvConfiguration CsvConfiguration { get; }
    }
}