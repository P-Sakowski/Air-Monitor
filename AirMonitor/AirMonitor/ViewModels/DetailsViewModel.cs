using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
                    if (caqi < 70)
                    {
                        this.caqiShortInfo = "Świetna jakość!";
                        this.caqiLongInfo = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
                    }
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
                    this.pm25Percentage = 4 * pm25;
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
                    this.pm10Percentage = 2 * pm10;
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
        public DetailsViewModel()
        {
            this.CAQI = 57;
            this.PM25 = 34;
            this.PM10 = 67;
            this.Pressure = 1026;
            this.Humidity = 95;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
