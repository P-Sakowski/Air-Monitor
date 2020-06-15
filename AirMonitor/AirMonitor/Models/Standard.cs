using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class Standard
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public decimal Limit { get; set; }
        public decimal Percent { get; set; }

        public Standard()
        {

        }
    }
}
