using AirMonitor.ViewModels;
using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel ViewModel => BindingContext as HomeViewModel;
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);

        }
        void GoToMeasurementDetails(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            ViewModel.NavigateToPage(e.Item as Measurement);
        }
    }
}