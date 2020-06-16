using Newtonsoft.Json;
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
        public string Sponsor { get; set; }

        public InstallationEntity()
        {

        }
        public InstallationEntity(Installation installation)
        {
            this.Id = installation.Id.ToString();
            this.Elevation = installation.Elevation;
            this.Airly = installation.Airly;
            this.Location = JsonConvert.SerializeObject(installation.Location);
            this.Address = JsonConvert.SerializeObject(installation.Address);
            this.Sponsor = JsonConvert.SerializeObject(installation.Sponsor);
        }
    }
}
