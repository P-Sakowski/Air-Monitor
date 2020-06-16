using Newtonsoft.Json;
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
        public Installation(InstallationEntity installationEntity)
        {
            int.TryParse(installationEntity.Id, out int installationEntityId);
            Id = installationEntityId;
            Address = JsonConvert.DeserializeObject<Address>(installationEntity.Address);
            Location = JsonConvert.DeserializeObject<Location>(installationEntity.Location);
            Elevation = installationEntity.Elevation;
            Airly = installationEntity.Airly;
            Sponsor = JsonConvert.DeserializeObject<Sponsor>(installationEntity.Sponsor);
        }
    }
}
