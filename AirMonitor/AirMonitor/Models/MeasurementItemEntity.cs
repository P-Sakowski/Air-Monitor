﻿using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementItemEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public string Values { get; set; }
        public string Indexes { get; set; }
        public string Standards { get; set; }

        public MeasurementItemEntity()
        {

        }
        public MeasurementItemEntity(Measurement measurement)
        {
            FromDateTime = measurement.Current.FromDateTime;
            TillDateTime = measurement.Current.TillDateTime;
            Values = JsonConvert.SerializeObject(measurement.Current.Values);
            Indexes = JsonConvert.SerializeObject(measurement.Current.Indexes);
            Standards = JsonConvert.SerializeObject(measurement.Current.Standards);
        }
    }
}