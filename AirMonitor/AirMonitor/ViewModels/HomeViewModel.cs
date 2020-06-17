using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using AirMonitor.Models;
using System.Web;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Linq;

namespace AirMonitor.ViewModels
{
    class HomeViewModel: BaseViewModel
    {
        private bool activityIndicator;
        public bool ActivityIndicator
        {
            get { return activityIndicator; }
            set { SetProperty(ref activityIndicator, value); } 
        }
        public List<Measurement> Measurements { get; set; }
        private ObservableCollection<Measurement> _MeasurementList = new ObservableCollection<Measurement>();
        public ObservableCollection<Measurement> MeasurementList { get { return _MeasurementList; } set { _MeasurementList = value; OnPropertyChanged(nameof(MeasurementList)); } }
        public INavigation Navigation { get; set; }
        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            Initialize();
        }

        public ICommand Navigate => new Command<Measurement>(NavigateToPage);

        public void NavigateToPage(Measurement measurement)
        {
            this.Navigation.PushAsync(new DetailsPage(measurement));
        }

        public async Task Initialize()
        {
            IEnumerable<Measurement> measurements = null;
            IEnumerable<Installation> installations = null;
            ActivityIndicator = true; //loader: ON
            await Task.Run(async () =>
            {
                Location location = await GetLocation();
                if (App.Database.CheckDataTimeliness() == true)
                {
                    installations = App.Database.SelectInstallations();
                    measurements = App.Database.SelectMeasurements();
                }
                else
                {
                    installations = await GetInstallations(location);
                    measurements = await GetMeasurements(installations);
                }
                App.Database.InsertInstallations(installations);
                App.Database.InsertMeasurements(measurements);
            });
            ActivityIndicator = false; //loader: OFF
            Measurements = new List<Measurement>(measurements);
            MeasurementList = new ObservableCollection<Measurement>(Measurements);
        }

        public async Task<IEnumerable<Installation>> GetInstallations(Location location)
        {
            string lat = (location.Latitude +0.01).ToString(CultureInfo.InvariantCulture);
            string lng = (location.Longitude + 0.01).ToString(CultureInfo.InvariantCulture);
            string query = $"?lat={lat}&lng={lng}&maxDistanceKM=-1&maxResults=1";
            string url = App.URL + App.InstallationURL + query;

            IEnumerable<Installation> response = await GetHttpResponseAsync<IEnumerable<Installation>>(url);
            return response;
        }

        public static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(App.URL)
            };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", App.ApiKey);
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            return client;
        }

        public async Task<T> GetHttpResponseAsync<T>(string url)
        {
            try
            {
                HttpClient client = GetHttpClient();
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var RequestsLeft))
                {
                    System.Diagnostics.Debug.WriteLine($"Requests to end-of-daily-limit: {RequestsLeft?.FirstOrDefault()}");
                }

                switch ((int)response.StatusCode)
                {
                    case 200:
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(content);
                        return result;
                    default:
                        var errorContent = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Response error: {errorContent}");
                        return default;
                }
            }
            catch (JsonReaderException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return default;
        }

        public async Task<Location> GetLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium);
                Location location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

                return location;
            }
            catch (FeatureNotSupportedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (FeatureNotEnabledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (PermissionException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }

        private async Task<IEnumerable<Measurement>> GetMeasurements(IEnumerable<Installation> installations)
        {
            if(installations == null)
            {
                return null;
            }
            List<Measurement> measurements = new List<Measurement>();

            foreach (Installation installation in installations)
            {
                string query = $"?installationId={installation.Id}";
                string url = App.URL + App.MeasurementURL + query;

                Measurement response = await GetHttpResponseAsync<Measurement>(url);

                if (response != null)
                {
                    response.Installation = installation;
                    response.CAQIValue = (int)response.Current.Indexes?.FirstOrDefault(index => index.Name == "AIRLY_CAQI").Value;
                    measurements.Add(response);
                }
            }

            return measurements;
        }
    }
}