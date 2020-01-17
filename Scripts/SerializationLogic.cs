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
    }
}

#else

using Newtonsoft.Json;
using UnityEngine;

namespace PersistentData
{
    internal static class SerializationLogic
    {
        internal static void Set<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, json);
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
            => PlayerPrefs.DeleteKey(key);

        internal static void DeleteAll()
            => PlayerPrefs.DeleteAll();
    }
}

#endif