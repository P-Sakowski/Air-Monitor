using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class Standard
    {
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public decimal Limit { get; set; }
        public decimal Percent { get; set; }
    }
}
