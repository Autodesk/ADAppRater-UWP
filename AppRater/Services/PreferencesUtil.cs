//  (C) Copyright 2016 by Autodesk, Inc.
// 
// 
//  The information contained herein is confidential, proprietary
//  to Autodesk,  Inc.,  and considered a trade secret as defined
//  in section 499C of the penal code of the State of California.
//  Use of  this information  by  anyone  other  than  authorized
//  employees of Autodesk, Inc.  is granted  only under a written
//  non-disclosure agreement,  expressly  prescribing  the  scope
//  and manner of such use.

using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using AppData = Windows.Storage.ApplicationData;

namespace AppRater.Services
{
    public static class PreferencesUtil
    {
        private static string _userId;

        public static void Initialize(string userId)
        {
            _userId = userId;
        }

        public static string GetString(string key, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            System.Object prefVal = null;
            lock (AppData.Current.LocalSettings)
            {
                prefVal = AppData.Current.LocalSettings.Values[prefKey];
            }
            return (string.IsNullOrEmpty((string)prefVal)) ? string.Empty : (string)prefVal;
        }

        public static void SetString(string key, string val, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            lock (AppData.Current.LocalSettings)
            {
                AppData.Current.LocalSettings.Values[prefKey] = val;
            }
        }

        public static int GetInteger(string key, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            System.Object prefVal = null;
            lock (AppData.Current.LocalSettings)
            {
                prefVal = AppData.Current.LocalSettings.Values[prefKey];
            }
            return (int?)prefVal ?? 0;
        }

        public static void SetInteger(string key, int val, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            lock (AppData.Current.LocalSettings)
            {
                AppData.Current.LocalSettings.Values[prefKey] = val;
            }
        }

        public static bool GetBoolean(string key, bool defaultVal = false, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);

            System.Object prefVal = null;
            lock (AppData.Current.LocalSettings)
            {
                prefVal = AppData.Current.LocalSettings.Values[prefKey];
                if (prefVal == null && defaultVal)
                {
                    prefVal = AppData.Current.LocalSettings.Values[prefKey] = defaultVal;
                }
            }

            return (bool?)prefVal ?? false;
        }

        public static void SetBoolean(string key, bool val, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            lock (AppData.Current.LocalSettings)
            {
                AppData.Current.LocalSettings.Values[prefKey] = val;
            }
        }

        public static bool HasKey(string key, bool isGlobalPref = false)
        {
            string prefKey = GetPrefKey(key, isGlobalPref);
            lock (AppData.Current.LocalSettings)
            {
                return AppData.Current.LocalSettings.Values.Keys.Contains(prefKey);
            }
        }

        private static string GetPrefKey(string key, bool isGlobalPref)
        {
            return isGlobalPref ? key : string.Format(_userId + "_" + key);
        }

        // Methods from the old SettingService class

        public static async Task<string> ReadTextFileAsync(string path)
        {
            var folder = AppData.Current.LocalFolder;
            var file = await folder.GetFileAsync(path);
            return await FileIO.ReadTextAsync(file);
        }

        public static async void WriteToTextFileAsync(string fileName, string contents)
        {
            var folder = AppData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, contents);
        }

        public static void SaveSettings(string key, string contents)
        {
            lock (AppData.Current.LocalSettings)
            {
                AppData.Current.LocalSettings.Values[key] = contents;
            }
        }

        public static string LoadSettings(string key)
        {
            lock (AppData.Current.LocalSettings)
            {
                var settings = AppData.Current.LocalSettings;

                if (!settings.Values.ContainsKey(key))
                {
                    return string.Empty;
                }

                return settings.Values[key].ToString();
            }
        }

        public static string LoadSettingsInContainer(string container, string key)
        {
            lock (AppData.Current.LocalSettings)
            {
                var localSetting = AppData.Current.LocalSettings;

                if (!localSetting.Containers.Keys.Contains(key) || !localSetting.Containers[key].Values.ContainsKey(container))
                {
                    return string.Empty;
                }

                return (string)localSetting.Containers[key].Values[container];
            }
        }

        public static void SaveSettingsInContainer(string container, string key, string contents)
        {
            lock (AppData.Current.LocalSettings)
            {
                var localSetting = AppData.Current.LocalSettings;

                localSetting.CreateContainer(container, ApplicationDataCreateDisposition.Always);

                if (localSetting.Containers.ContainsKey(container))
                {
                    localSetting.Containers[container].Values[key] = contents;
                }
            }
        }

        public static void DeleteSettingsInContainer(string container)
        {
            lock (AppData.Current.LocalSettings)
            {
                var localSetting = AppData.Current.LocalSettings;

                localSetting.DeleteContainer(container);
            }
        }
    }
}