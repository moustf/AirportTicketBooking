using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AirportTicketBooking
{
    public class CSVReaderService
    {
        public StreamReader StreamReader { get; private set; }
        public CsvReader CsvReader { get; private set; }
        
        public CSVReaderService(string currentDirectory, CsvConfiguration csvConfiguration, string fileName)
        {
            StreamReader = new StreamReader($@"{currentDirectory}/DataStore/{fileName}.csv");
            CsvReader = new CsvReader(StreamReader, csvConfiguration);
        }
    }
}