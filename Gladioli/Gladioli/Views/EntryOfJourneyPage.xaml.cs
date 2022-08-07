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
    public partial class EntryOfJourneyPage : ContentPage
    {
        public EntryOfJourneyPage()
        {
            InitializeComponent();
            var Item = new Journey();
            BindingContext = Item;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var Item = (Journey)BindingContext;
            GladioliDatabase database = await GladioliDatabase.Instance;
            await database.SaveItemAsync(Item);
            await Navigation.PopAsync();


        }
    }
}