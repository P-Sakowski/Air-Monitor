using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class MeasurementItem
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public IEnumerable<ParameterValue> Values { get; set; }
        public IEnumerable<Index> Indexes { get; set; }
        public IEnumerable<Standard> Standards { get; set; }

        public MeasurementItem()
        {

        }
        public MeasurementItem(MeasurementItemEntity measurementItemEntity, List<ParameterValue> parameterValues, List<Index> indices, List<Standard> standards)
        {
            FromDateTime = measurementItemEntity.FromDateTime;
            TillDateTime = measurementItemEntity.TillDateTime;
            Values = parameterValues;
            Indexes = indices;
            Standards = standards;
        }
    }
}
