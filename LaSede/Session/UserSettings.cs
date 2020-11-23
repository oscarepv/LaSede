using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaSede.Session
{
    class UserSettings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string userName
        {
            get => AppSettings.GetValueOrDefault(nameof(userName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(userName), value);
        }

        public static string userId
        {
            get => AppSettings.GetValueOrDefault(nameof(userId), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(userId), value);
        }

        public static string tiposuario
        {
            get => AppSettings.GetValueOrDefault(nameof(tiposuario), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(tiposuario), value);
        }

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
