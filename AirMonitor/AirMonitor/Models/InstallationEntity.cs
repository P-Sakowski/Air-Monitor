using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    public class InstallationEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public decimal Elevation { get; set; }
        public bool Airly { get; set; }
        public Sponsor Sponsor { get; set; }

        public InstallationEntity()
        {

        }
    }
}
