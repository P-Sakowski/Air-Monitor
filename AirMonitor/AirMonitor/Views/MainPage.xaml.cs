using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AirMonitor
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Help_Click(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Indeks jakości powietrza jest wykorzystywany przez agencje rządowe do informowania opinii publicznej o tym, jak zanieczyszczone jest obecnie powietrze lub jak przewiduje się jego zanieczyszczenie. Zagrożenia dla zdrowia publicznego rosną wraz ze wzrostem CAQI.", "Zamknij");
        }
    }
}
