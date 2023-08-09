using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AirportTicketBooking
{
    public class CSVWriterService
    {
        public StreamWriter StreamWriter { get; private set; }
        
        public CsvWriter CsvWriter { get; private set; }
        
        public CSVWriterService(string currentDirectory, CsvConfiguration csvConfiguration, string fileName)
        {
            var stream = File.Open($@"{currentDirectory}/DataStore/{fileName}.csv", FileMode.Append, FileAccess.Write);
            StreamWriter = new StreamWriter(stream);
            CsvWriter = new CsvWriter(StreamWriter, csvConfiguration);
        }
    }
}