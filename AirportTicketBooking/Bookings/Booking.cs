using System;
using CsvHelper;

namespace AirportTicketBooking.Bookings
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime DateOfBooking { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public int SeatsNumber { get; set; }
        
        public void CancelBooking(int bookingId, CSVIOService csvioService, CSVReaderService bookingCsvReader, WriteToCsvFile writeToCsvFile)
        {
            var bookings = csvioService.GetAllRecords<Booking>(bookingCsvReader.CsvReader);
            
            bookingCsvReader.StreamReader.Close();
            
            writeToCsvFile.WriteDataToCsv(bookings, "Booking");
        }
        public Booking EditBooking(int bookingId, CSVIOService csvioService, CsvReader csvReader)
        {
            return csvioService.SearchForRecord<Booking>("BookingId", bookingId.ToString(), csvReader);
        }
        public override string ToString()
        {
            return $@"A booking with an id of: {BookingId}, and booked in {DateOfBooking}, with {SeatsNumber} of seats is available!";
        }
    }
}