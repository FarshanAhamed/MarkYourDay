
using MarkYourDay.Interfaces;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}