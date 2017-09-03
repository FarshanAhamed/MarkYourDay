
using MarkYourDay.Interfaces;
using System;
using MarkYourDay.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using MarkYourDay.Services;

namespace MarkYourDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            DependencyService.Get<IStatusBar>().HideStatusBar();
            LoginButton.Clicked += Login_Clicked;           
        }

        public async void Login_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && (!string.IsNullOrWhiteSpace(Password.Text)))
            {
                if (await IsUserValid(Username.Text, Password.Text))
                {
                    await Navigation.PushAsync(new Punch(Username.Text));
                }
                else
                    await DisplayAlert("Incorrect", "Incorrect Login Details!\nPlease Fill in details and Try Again", "OK");
            }
            else
                await DisplayAlert("Fill Details", "Please Fill all details and Try Again", "OK");
        }

       /* public async Task<bool> IsUserValid(string username, string password)
        {
            bool result = true;
            var item = await UserServices.GetUser(username, password);
            if (item.data == null)
            {
                result = false;
            }
            return result;
        }*/
    }
}