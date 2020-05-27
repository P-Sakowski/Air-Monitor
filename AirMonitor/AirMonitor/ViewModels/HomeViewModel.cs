using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        public INavigation Navigation { get; set; }
        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
        public ICommand Navigate => new Command(NavigateToPage);

        private void NavigateToPage()
        {
            this.Navigation.PushAsync(new DetailsPage());
        }
    }
}