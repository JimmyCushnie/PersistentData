#if UNITY_EDITOR || UNITY_STANDALONE

using SUCC;

namespace PersistentData
{
    internal static class SerializationLogic
    {
        static DataFile File = new DataFile(nameof(GameValues));

        internal static void Set<T>(string key, T value) 
            => File.Set(key, value);

        internal static T Get<T>(string key, T defaultValue) 
            => File.Get(key, defaultValue);

        internal static bool KeyExists(string key)
            => File.KeyExists(key);

        internal static void DeleteKey(string key)
            => File.DeleteKey(key);

        internal static void DeleteAll()
        {
            System.IO.File.Delete(File.FilePath);
            File = new DataFile(nameof(GameValues));
        }

        public static bool AutoSave
        {
            get => File.AutoSave;
            set => File.AutoSave = value;
        }
        public static void SaveAllDataToDisk() 
            => File.SaveAllData();
    }
}

#else

using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace PersistentData
{
    internal static class SerializationLogic
    {
        internal static void Set<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, json);

            if (AutoSave)
                PlayerPrefs.Save();
        }

        internal static T Get<T>(string key, T defaultValue)
        {
            if (KeyExists(key))
            {
                string json = PlayerPrefs.GetString(key);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                Set(key, defaultValue);
                return defaultValue;
            }
        }

        internal static bool KeyExists(string key)
            => PlayerPrefs.HasKey(key);

        internal static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);

            if (AutoSave)
                PlayerPrefs.Save();
        }

        internal static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();

            if (AutoSave)
                PlayerPrefs.Save();
        }

        public static bool AutoSave { get; set; } = true;
        public static void SaveAllDataToDisk() 
            => PlayerPrefs.Save(); // Worth noting: Unity automatically calls this during OnApplicationQuit
    }
}

#endif