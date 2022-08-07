using Gladioli.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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


        private async void file(object sender, EventArgs e)
        {
            await PickerPhotoAsync();
        }
        string PhotoPath = null;
        async Task PickerPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
            async Task LoadPhotoAsync(FileResult photo)
            {
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }
                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (Stream stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                }
                PhotoPath = newFile;
            }
        }

         async void OnDeleteClicked(object sender, EventArgs e)
        {
            var Item = (Journey)BindingContext;
            GladioliDatabase database = await GladioliDatabase.Instance;
            await database.DeleteItemAsync(Item);
            await Navigation.PopAsync();

        }
    }
}