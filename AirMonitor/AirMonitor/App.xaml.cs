using AirMonitor.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AirMonitor;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Linq;

namespace AirMonitor
{
    public partial class App : Xamarin.Forms.Application
    {
        public static string URL { get; set; }
        public static string MeasurementURL { get; set; }
        public static string InstallationURL { get; set; }
        public static string ApiKey { get; set; }
        public static DatabaseHelper Database { get; set; }
        public App()
        {
            InitializeComponent();

            InitTask();

            Database = new DatabaseHelper();

            MainPage = new MainTabbedPage();
        }

        public async Task InitTask()
        {
            await LoadJSON();
        }

        public static async Task LoadJSON()
        {
            Assembly assembly = typeof(App).Assembly;
            string[] resourceNames = assembly.GetManifestResourceNames();
            string config = resourceNames.FirstOrDefault(resource => resource.Contains("config.json"));

            using (Stream stream = assembly.GetManifestResourceStream(config))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    JObject dynamicJson = JObject.Parse(json);

                    URL = dynamicJson["URL"].Value<string>();
                    MeasurementURL = dynamicJson["MeasurementURL"].Value<string>();
                    InstallationURL = dynamicJson["InstallationURL"].Value<string>();
                    ApiKey = dynamicJson["ApiKey"].Value<string>();
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
