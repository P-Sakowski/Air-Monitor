using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class Measurement
    {
        public MeasurementItem Current { get; set; }
        public IEnumerable<MeasurementItem> History { get; set; }
        public IEnumerable<MeasurementItem> Forecast { get; set; }
    }
}
