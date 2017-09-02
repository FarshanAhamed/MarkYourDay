using MarkYourDay.Helpers;
using MarkYourDay.Interfaces;
using MarkYourDay.Views;

using Xamarin.Forms;

namespace MarkYourDay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Get<IStatusBar>().HideStatusBar();

            if (LoginHelper.CheckLoggedIn())
            {
                MainPage = new NavigationPage(new Punch(Settings.Username));
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
   
}
