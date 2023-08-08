using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AirportTicketBooking
{
    public sealed class CSVConfiguration
    {
        private static readonly Lazy<CSVConfiguration> Lazy = new Lazy<CSVConfiguration>(() => new CSVConfiguration());
        
        private static readonly IEnumerable<string> FullHomeDir = Directory.GetCurrentDirectory().Split('/').TakeWhile(dir => dir != "bin");
        
        public CsvConfiguration CsvConfiguration { get; } = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        
        public string CurrentDirectory { get; } = String.Join("/", FullHomeDir);
        
        private CSVConfiguration() {}
        
        public static CSVConfiguration Instance => Lazy.Value;
    }
}