using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public static class GameValues
    {
        public static void Set<T>(string key, T value) 
            => SerializationLogic.Set(key, value);

        public static T Get<T>(string key, T defaultValue) 
            => SerializationLogic.Get(key, defaultValue);

        public static bool KeyExists(string key)
            => SerializationLogic.KeyExists(key);

        public static void DeleteKey(string key)
            => SerializationLogic.DeleteKey(key);

        public static void DeleteAll(string key)
            => SerializationLogic.DeleteAll();
    }
}