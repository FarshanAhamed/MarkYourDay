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
        private const string TokenIdentifier = "Token";
        private const string UserIdIdentifier = "UserId";
        private const string CheckedTimeIdentifier = "CheckedTime";
        private const string CheckedStatusIdentifier = "CheckedStatus";
        #endregion

        public static string CheckedTime
        {
            get
            {
                return AppSettings.GetValueOrDefault(CheckedTimeIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CheckedTimeIdentifier, value);
            }
        }
        public static string CheckedStatus
        {
            get
            {
                return AppSettings.GetValueOrDefault(CheckedStatusIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CheckedStatusIdentifier, value);
            }
        }
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
        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TokenIdentifier, value);
            }
        }
        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdIdentifier, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdIdentifier, value);
            }
        }

        public static void Clear()
        {
            Username = stringDefault;
            UserId = stringDefault;
            Token = stringDefault;
            CheckedStatus = stringDefault;
            CheckedTime = stringDefault;
        }
    }
}