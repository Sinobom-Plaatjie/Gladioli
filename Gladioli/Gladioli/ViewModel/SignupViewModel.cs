using Gladioli.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gladioli.ViewModel
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name;
        public string surname;
        public string username;
        public string password;
        public string confirmPassword;

        Regex names = new Regex("[^a-zA-Z]");

        public string Names
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Names"));
            }
        }
      
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Surname"));
            }
        }
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SignupSubmitCommand { get; set; }
        public SignupViewModel()
        {
            SignupSubmitCommand = new Command(OnSubmitAsync);
        }
        public async void OnSubmitAsync()
        {
            Regex names = new Regex("[^a-zA-Z]");

            if (IsValidated())
            {
                SaveSignUp();

                await App.Current.MainPage.Navigation.PushAsync(new Login());
            }
        }

        public bool IsValidated()
        {
            if (string.IsNullOrEmpty(Username)
                && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword)
                && string.IsNullOrEmpty(Names) && string.IsNullOrEmpty(Surname))
            {
                MessagingCenter.Send(this, "Signup Alert", "Please fill-up the form");
                return false;
            }
            else
            {
               
              if (!names.IsMatch(Names) && !names.IsMatch(Surname))
              {
               return true;
              }
              
              MessagingCenter.Send(this, "Signup Alert", "Name and surname must be letters only");        
            }
            return false;
        }

        public async void SaveSignUp()
        {
            UserDatabase database = await UserDatabase.Instance;

            var data = new SigningDataModel() { Names = this.Names, Password = this.Password, Surname = this.Surname, Username = this.Username };
            await database.SaveItemAsync(data);
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
    }
}   
            