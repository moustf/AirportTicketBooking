using System;
using System.Collections.Generic;
using AirportTicketBooking.Bookings;
using AirportTicketBooking.Flights;
using AirportTicketBooking.Passengers;
using AirportTicketBooking.Managers;
using CsvHelper;

namespace AirportTicketBooking
{
    public class WriteToCsvFile
    {
        private readonly CSVWriterService _csvWriterService;

        public WriteToCsvFile(CSVWriterService csvWriterService)
        {
            _csvWriterService = csvWriterService;
        }
        
        public void WriteDataToCsv(object dataObject, string fileName)
        {
            var objType = dataObject.GetType();
            if (
                objType.Name != nameof(Passenger)
                && objType.Name != nameof(Manager)
                && objType.Name != nameof(Flight)
                && objType.Name != nameof(Booking)
            )
            {
                throw new Exception("The objects types are not consistent!");
            }

            _csvWriterService.CsvWriter.WriteRecord(dataObject);
            _csvWriterService.CsvWriter.NextRecord();
            
            _csvWriterService.StreamWriter.Close();
        }
        
        public void WriteDataToCsv(IEnumerable<object> objects, string fileName)
        {
            var objType = objects.GetType();
            if (
                objType.Name != nameof(Passenger)
                && objType.Name != nameof(Manager)
                && objType.Name != nameof(Flight)
                && objType.Name != nameof(Booking)
            )
            {
                throw new Exception("The objects types are not consistent!");
            }

            _csvWriterService.CsvWriter.WriteRecords(objects);
            
            _csvWriterService.StreamWriter.Close();
        }
    }
}