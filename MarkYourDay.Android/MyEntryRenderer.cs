using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using MarkYourDay.Droid;
using MarkYourDay.Helpers;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace MarkYourDay.Droid
{
    class MyEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Resources.GetDrawable(Resource.Drawable.NoBorderStyle);
                // Control?.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.LightGray));
                Control.SetPadding(15,15,15,15);
                                
            }
        }
    }
}