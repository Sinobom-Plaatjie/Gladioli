using Gladioli.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gladioli
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }



          private async void FindMeButton(object sender, EventArgs e)
          {
            await Navigation.PushAsync(new CurrentLocationPage());
          }

         private async void JourneyButtonAsync(object sender, EventArgs e)
          {
             await Navigation.PushAsync(new MyJourneyPage());
          }
    }
}  