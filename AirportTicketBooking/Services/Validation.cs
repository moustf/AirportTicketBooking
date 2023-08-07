using System;
using System.Collections.Generic;

namespace AirportTicketBooking.Services
{
    public class Validation
    {
        public List<string> ValidateFlights(string[] csvLines)
        {
            var errors = new List<string>();

            foreach (var line in csvLines)
            {
                var values = line.Split(',');

                if (!int.TryParse(values[0], out var res))
                {
                    errors.Add($"{values[0]}: The first column must be the flight id of type integer!");
                }

                if (values[1].Split(':').Length <= 0)
                {
                    errors.Add(
                        $"{values[0]}: The name of the flight must implement this format: 'FlightId:DepartureCountry:ArrivalCountry:FlightClass'.");
                }

                if (values[2] != "economy" && values[2] != "business" && values[2] != "first class")
                {
                    errors.Add($"{values[0]}: The flight class can either be 'economy', 'business', or 'first class'.");
                }

                if (!decimal.TryParse(values[3], out var dRes))
                {
                    errors.Add($"{values[0]}: The money must be decimal type, with floating point numbers.");
                }

                if (string.IsNullOrWhiteSpace(values[4]))
                {
                    errors.Add($"{values[0]}: Departure country can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[5]))
                {
                    errors.Add($"{values[0]}: Arrival country can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[6]))
                {
                    errors.Add($"{values[0]}: Departure airport can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[7]))
                {
                    errors.Add($"{values[0]}: Arrival airport can't be null!");
                }

                if (string.IsNullOrWhiteSpace(values[8]))
                {
                    errors.Add($"{values[0]}: Airlines name can't be null!");
                }

                if (!DateTime.TryParse(values[9], out var dtRes))
                {
                    errors.Add(
                        $"{values[0]}: Departure time must be a valid time stamp following this format 'mm/dd/yyyy hh:mm:ss'.");
                }
            }

            return errors;
        }
    }
}