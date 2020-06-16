using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CurrentID { get; set; }
        public string History { get; set; }
        public string Forecast { get; set; }
        public int InstallationID { get; set; }
        public int CAQIValue { get; set; }

        public MeasurementEntity()
        {

        }
        public MeasurementEntity(Measurement measurement, MeasurementItemEntity measurementItemEntity)
        {
            CurrentID = measurementItemEntity.Id;
            History = JsonConvert.SerializeObject(measurement.History);
            Forecast = JsonConvert.SerializeObject(measurement.Forecast);
            InstallationID = measurement.Installation.Id;
        }
    }
}
