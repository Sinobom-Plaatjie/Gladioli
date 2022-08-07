using Gladioli.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gladioli.ViewModel
{
    public class SigninViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        public string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public ICommand SubmitCommand { get; set; }

        public SigninViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                MessagingCenter.Send(this, "Login Alert", "Please fill-up the form");
            }
            else if (string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                MessagingCenter.Send(this, "Login Alert", "Please enter username the form");
            }
            else if (!string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                MessagingCenter.Send(this, "Login Alert", "Please enter password the form");
            }
            else
            {
               
                var db = await UserDatabase.Instance;

                var login = await db.Login(Username, Password);

                if (login == true)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                
                else
                {
                    MessagingCenter.Send(this, "Login Alert", "Wrong username or password");
                }
            }
        }
    }
}
