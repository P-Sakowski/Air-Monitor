using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : BaseViewModel
    {
        private int caqi, pm25, pm10, humidity, pm25Percentage, pm10Percentage, pressure;
        private string caqiShortInfo, caqiLongInfo;
        public int CAQI
        {
            set
            {
                if (caqi != value)
                {
                    caqi = value;
                    OnPropertyChanged("CAQI");
                }
            }
            get
            {
                return caqi;
            }
        }
        public string CAQIShortInfo{
            set
            {
                if (caqiShortInfo != value)
                {
                    caqiShortInfo = value;
                    OnPropertyChanged("CAQIShortInfo");
                }
            }
            get
            {
                return caqiShortInfo;
            }
        }
        public string CAQILongInfo
        {
            set
            {
                if (caqiLongInfo != value)
                {
                    caqiLongInfo = value;
                    OnPropertyChanged("CAQILongInfo");
                }
            }
            get
            {
                return caqiLongInfo;
            }
        }
        public int PM25
        {
            set
            {
                if (pm25 != value)
                {
                    pm25 = value;
                    OnPropertyChanged("PM25");
                }
            }
            get
            {
                return pm25;
            }
        }
        public int PM25Percentage
        {
            set
            {
                if (pm25Percentage != value)
                {
                    pm25Percentage = value;
                    OnPropertyChanged("PM25Percentage");
                }
            }
            get
            {
                return pm25Percentage;
            }
        }
        public int PM10
        {
            set
            {
                if (pm10 != value)
                {
                    pm10 = value;
                    OnPropertyChanged("PM10");
                }
            }
            get
            {
                return pm10;
            }
        }
        public int PM10Percentage
        {
            set
            {
                if (pm10Percentage != value)
                {
                    pm10Percentage = value;
                    OnPropertyChanged("PM10Percentage");
                }
            }
            get
            {
                return pm10Percentage;
            }
        }
        public int Humidity
        {
            set
            {
                if (humidity != value)
                {
                    humidity = value;
                    OnPropertyChanged("Humidity");
                }
            }
            get
            {
                return humidity;
            }
        }
        public int Pressure
        {
            set
            {
                if (pressure != value)
                {
                    pressure = value;
                    OnPropertyChanged("Pressure");
                }
            }
            get
            {
                return pressure;
            }
        }
        public DetailsViewModel(Measurement measurement)
        {
            this.CAQI = (int)measurement.Current.Indexes?.FirstOrDefault(index => index.Name == "AIRLY_CAQI").Value;
            this.PM25 = (int)measurement.Current.Values?.FirstOrDefault(value => value.Name == "PM25").Value;
            this.PM10 = (int)measurement.Current.Values?.FirstOrDefault(value => value.Name == "PM10").Value;
            this.Pressure = (int)measurement.Current.Values?.FirstOrDefault(value => value.Name == "PRESSURE").Value;
            this.Humidity = (int)measurement.Current.Values?.FirstOrDefault(value => value.Name == "HUMIDITY").Value;
            this.PM25Percentage = (int)measurement.Current.Standards?.FirstOrDefault(standard => standard.Pollutant == "PM25").Percent;
            this.PM10Percentage = (int)measurement.Current.Standards?.FirstOrDefault(standard => standard.Pollutant == "PM10").Percent;
            this.caqiShortInfo = measurement.Current.Indexes?.FirstOrDefault(index => index.Name == "AIRLY_CAQI").Description;
            this.caqiLongInfo = measurement.Current.Indexes?.FirstOrDefault(index => index.Name == "AIRLY_CAQI").Advice; 
        }
    }
    public class IntToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = (int)value / 100.0d;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = System.Convert.ToInt32(100 * (double)value);
            return result;
        }
    }
}
