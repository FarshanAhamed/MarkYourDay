﻿using Android.App;
using Android.Views;
using MarkYourDay.Interfaces;
using Xamarin.Forms;
using MarkYourDay.Droid;
using MarkYourDay.Views;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarImplementation))]
[assembly: Xamarin.Forms.Dependency(typeof(CloseApp))]
namespace MarkYourDay.Droid
{
    public class StatusBarImplementation: IStatusBar
    {
        WindowManagerFlags _originalFlags;

        #region IStatusBar implementation

        public void HideStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }
        public void ShowStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }
        #endregion
    }

    public class CloseApp : ICloseApp
    {
        public void ClosetheApp()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}