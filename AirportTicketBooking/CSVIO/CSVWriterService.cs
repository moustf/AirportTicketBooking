using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AirportTicketBooking
{
    public class CSVWriterService
    {
        public CSVWriterService(string currentDirectory, CsvConfiguration csvConfiguration, string fileName)
        {
            var stream = File.Open($@"{currentDirectory}/DataStore/{fileName}.csv", FileMode.Append);
            var writer = new StreamWriter(stream);
            CsvWriter = new CsvWriter(writer, csvConfiguration);
        }
        
        public CsvWriter CsvWriter { get; private set; }
    }
}