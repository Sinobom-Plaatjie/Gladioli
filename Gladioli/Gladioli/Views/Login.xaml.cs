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
    public partial class Login : ContentPage
    {
        public SigninViewModel userModel;
        public Login()
        {
            InitializeComponent();
            userModel = new SigninViewModel();


            MessagingCenter.Subscribe<SigninViewModel, string>(this, "Login Alert", (sender, username) =>
            {
                DisplayAlert("", username, "Ok");

            });
            this.BindingContext = userModel;
            entUsername.Completed += (object sender, EventArgs e) =>
            {
                entPassword.Focus();
            };
            entPassword.Completed += (object sender, EventArgs e) =>
            {
                userModel.SubmitCommand.Execute(null);
            };
        }
        private async void Signup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
           
}