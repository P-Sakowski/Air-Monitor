using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AirMonitor
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
        private void Button_Help_Click(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Indeks jakości powietrza jest wykorzystywany przez agencje rządowe do informowania opinii publicznej o tym, jak zanieczyszczone jest obecnie powietrze lub jak przewiduje się jego zanieczyszczenie. Zagrożenia dla zdrowia publicznego rosną wraz ze wzrostem CAQI.", "Zamknij");
        }
    }
}
