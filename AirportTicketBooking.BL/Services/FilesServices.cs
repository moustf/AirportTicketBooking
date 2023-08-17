using System.IO;

namespace AirportTicketBooking.Services
{
    public class FileServices
    {
        public string[] ReadCsvFile(string pathToFile)
        {
            var lines = File.ReadAllLines(pathToFile);

            return lines;
        }

        public void WriteToCsvFile(string filePath, string[] lines)
        {
            File.WriteAllLines(filePath, lines);
        }
    }
}