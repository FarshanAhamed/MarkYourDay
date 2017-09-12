
using MarkYourDay.Interfaces;
using System;
using MarkYourDay.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                    await Navigation.PushAsync(new Punch(Username.Text));
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


    }

   
}