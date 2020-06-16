﻿using Newtonsoft.Json;
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
        public Installation Installation { get; set; }
        public int CAQIValue { get; set; }

        public Measurement()
        {

        }
        public Measurement(MeasurementEntity measurementEntity, Installation installation, MeasurementItem measurementItem)
        {
            Current = measurementItem;
            History = JsonConvert.DeserializeObject<IEnumerable<MeasurementItem>>(measurementEntity.History);
            Forecast = JsonConvert.DeserializeObject<IEnumerable<MeasurementItem>>(measurementEntity.Forecast);
            Installation = installation;
        }
    }
}
