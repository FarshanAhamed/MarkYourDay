using MarkYourDay.Helpers;
using MarkYourDay.Interfaces;
using MarkYourDay.Views;

using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace MarkYourDay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Get<IStatusBar>().HideStatusBar();
           

            if (LoginHelper.CheckLoggedIn())
            {
                MainPage = new NavigationPage(new Punch());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected override void OnStart()
        {
            //Azure Mobile Centre analytics
            MobileCenter.Start("android=ba53d232-81be-479a-bf00-9e2d4866581b;" +
                    "ios=0184baba-7699-454f-b36a-70eeb2504fcc",
                    typeof(Analytics), typeof(Crashes)); 
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
