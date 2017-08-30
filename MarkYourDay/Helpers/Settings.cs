// Helpers/Settings.cs
using Plugin.SecureStorage;
using Plugin.SecureStorage.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MarkYourDay.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}
        private static ISecureStorage SecureSettings
        {
            get
            {
                return CrossSecureStorage.Current;
            }
        }

        #region Setting Constants

      
		private static readonly string stringDefault = string.Empty;
        private const string UserNameIdentifier = "Username";
        private const string MarkIdentifier = "AtFantacode";
        private const string OKIdentifier = "OK";
        private const string StartTimeIdentifier = "StartTime";
        private const string StopTimeIdentifier = "StopTime";
        #endregion

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameIdentifier, value);
            }
        }


        public static string AtFantacode
        {
            get
            {
                return AppSettings.GetValueOrDefault(MarkIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(MarkIdentifier, value);
            }
        }

        public static string OK
        {
            get
            {
                return AppSettings.GetValueOrDefault(OKIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(OKIdentifier, value);
            }
        }

        public static string StartTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(StartTimeIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(StartTimeIdentifier, value);
            }
        }

        public static string StopTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(StopTimeIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(StopTimeIdentifier, value);
            }
        }

    }
}