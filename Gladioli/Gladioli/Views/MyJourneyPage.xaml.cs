using Gladioli.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gladioli.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyJourneyPage : ContentPage
    {
        public MyJourneyPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            GladioliDatabase database = await GladioliDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
        }
       
        async void OnListAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryOfJourneyPage
            {
                BindingContext = new Journey()
            });
        }
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
             if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EntryOfJourneyPage
                {
                    BindingContext = e.SelectedItem as Journey
                });
            }
        }

        private void AddNewJourney(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EntryOfJourneyPage());
        }
    }
}