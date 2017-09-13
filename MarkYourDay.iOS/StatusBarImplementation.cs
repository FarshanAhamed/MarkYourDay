using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkYourDay.Interfaces;
using Foundation;
using UIKit;
using MarkYourDay.iOS;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarImplementation))]
namespace MarkYourDay.iOS
{
    public class StatusBarImplementation : IStatusBar
    {
        public StatusBarImplementation()
        {
        }

        #region IStatusBar implementation

        public void HideStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
        #endregion
    }
    public class CloseApp : ICloseApp
    {
        public void ClosetheApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}