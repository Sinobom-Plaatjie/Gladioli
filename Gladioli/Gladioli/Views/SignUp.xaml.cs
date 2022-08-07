using Gladioli.ViewModel;
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
    public partial class SignUp : ContentPage
    {
        public SignupViewModel signupVM = new SignupViewModel();
        public SignUp()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<SignupViewModel, string>(this, "Signup Alert", (sender, username) => {
                DisplayAlert("", username, "ok");
            });
            this.BindingContext = signupVM;
        }

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}