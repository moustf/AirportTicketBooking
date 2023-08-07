using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AirportTicketBooking
{
    public class CSVReaderService
    {
        public CSVReaderService(string currentDirectory, CsvConfiguration csvConfiguration, string fileName)
        {
            var streamReader = new StreamReader($@"{currentDirectory}/DataStore/{fileName}.csv");
            CsvReader = new CsvReader(streamReader, csvConfiguration);
        }
        
        public CsvReader CsvReader { get; private set; }
    }
}