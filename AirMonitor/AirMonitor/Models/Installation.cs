using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    public class Installation
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public Location Location { get; set; }
        public decimal Elevation { get; set; }
        public bool Airly { get; set; }
        public Sponsor Sponsor { get; set; }

        public Installation()
        {

        }
    }
}
