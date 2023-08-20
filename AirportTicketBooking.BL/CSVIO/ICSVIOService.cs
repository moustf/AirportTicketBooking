using AirportTicketBooking.Flights;
using CsvHelper;

namespace AirportTicketBooking
{
    public interface ICSVIOService
    {
        T SearchForRecord<T>(string property, string value, CsvReader csv);
        T[] GetAllRecords<T>(CsvReader csv);
        Flight SearchForFlightBy(string category, string value, Flight[] flights);
    }
}