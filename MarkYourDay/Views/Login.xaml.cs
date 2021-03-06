﻿
using MarkYourDay.Interfaces;
using System;
using MarkYourDay.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MarkYourDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage,INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();
            BindingContext = this;
            DependencyService.Get<IStatusBar>().HideStatusBar();
            LoginButton.Clicked += Login_Clicked;           
        }
        private bool _isBusy;

        public new bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                // again, this is very important
                OnPropertyChanged();
            }
        }
        public async void Login_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && (!string.IsNullOrWhiteSpace(Password.Text)))
            {
                IsBusy = !IsBusy;
                OnPropertyChanged("IsBusy");
                myStackLayout.IsVisible = !myStackLayout.IsVisible;
                if (await LoginHelper.IsUserValid(Username.Text.ToString(), Password.Text.ToString(),this))
                {
                    await Navigation.PushAsync(new Punch());
                }
                else
                {
                    myStackLayout.IsVisible = !myStackLayout.IsVisible;
                    IsBusy = !IsBusy;
                    OnPropertyChanged("IsBusy");
                }
            }
            else
                await DisplayAlert("Fill Details", "Please Fill all details and Try Again", "OK");
        }

       

        // this little bit is how we trigger the PropertyChanged notifier.
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected override bool OnBackButtonPressed()
        {
            Task<bool> action = DisplayAlert("Are you sure?", "Do you want to close the application?", "Yes", "No");
            action.ContinueWith(task =>
            {
                if (task.Result)
                {
                    DependencyService.Get<ICloseApp>().ClosetheApp();
                    base.OnBackButtonPressed();
                    //return false;
                }
            });
            return true;
                      
        }

    }

   
}