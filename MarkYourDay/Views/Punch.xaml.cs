using MarkYourDay.Helpers;
using MarkYourDay.Interfaces;
using MarkYourDay.Models;
using MarkYourDay.Services;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarkYourDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Punch :  ContentPage ,INotifyPropertyChanged
    {
       private string userText { get; set; }
        private string time { get; set; }
        WorkPlace fc;
        public static Double LATITUDE = 11.284108;
        public static Double LONGITUDE = 75.792672;

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public string UserText
        {
            get { return userText; }
            set { userText = value;
                OnPropertyChanged();
            }
        }
        public string Time
        {
            get { return time; }
            set { time = value;
                OnPropertyChanged();
            }
        }
        public void setPage()
        {
            switch (Settings.CheckedStatus)
            {
                case "CHECKED_OUT":
                    CheckInBtn.IsVisible = true;
                    CheckOutBtn.IsVisible = false;
                    break;
                case "CHECKED_IN":
                    CheckInBtn.IsVisible = false;
                    CheckOutBtn.IsVisible = true;
                    break;
                case "CHECKS_FULL":
                    CheckInBtn.IsEnabled = false;
                    CheckOutBtn.IsEnabled = false;
                    break;
                default:
                    CheckInBtn.IsVisible = true;
                    CheckOutBtn.IsVisible = false;
                    break;
            }
            //DependencyService.Get<IStatusBar>().ShowStatusBar();
            if (!string.IsNullOrWhiteSpace(Settings.Username))
            {
                userText = "Hey "+ Settings.Username + "!";
                OnPropertyChanged("UserText");
                if (!string.IsNullOrWhiteSpace(Settings.CheckedTime))
                {
                    time = Settings.CheckedTime;
                    OnPropertyChanged("Time");
                }
            }
        }
        public async void Init()
        {
            Loading();  
           await LoginHelper.GetToday();
           setPage();
            Loading();
        }

        public Punch(string Username)
        {
            
            InitializeComponent();
            Init();
            BindingContext = this;
            fc = new WorkPlace(LATITUDE, LONGITUDE);           


        }

        public async void Logout_Clicked(object sender, EventArgs args)
        {
            var res = await DisplayAlert("Are you Sure?", "Are you sure you want to logout", "OK", "Cancel");
            if (res)
            {
                Settings.Clear();
                await Navigation.PushAsync(new Login());
            }           
        }

        public async Task OnStartTapped(object sender,EventArgs args)
        {
            Loading();
            var result = await WhereAmI.GetLocation(this);
            if (result)
            {
                await AttendanceServices.CheckIn(this);
                setPage();
            }           
            Loading();
        }

        public async Task OnStopTapped(object sender,EventArgs args)
        {
            Loading();
            var result = await WhereAmI.GetLocation(this);
            if (result)
            {
                var res = await DisplayAlert("Are you Sure?", "Are you sure you want to leave Fantacode", "OK", "Cancel");
                if (res)
                {
                    await AttendanceServices.CheckOut(this);
                    setPage();
                }
            }          
            Loading();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void Loading()
        {
            myActivityIndicator.IsVisible = !myActivityIndicator.IsVisible;
            myGridView.IsVisible = !myGridView.IsVisible;
        }
    }
}