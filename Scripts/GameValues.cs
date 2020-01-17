using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public static class GameValues
    {
        /// <summary>
        /// Save some data, identified with a key
        /// </summary>
        /// <typeparam name="T">The type of data to save</typeparam>
        /// <param name="key">The key to identify the data with</param>
        /// <param name="value">The data to save</param>
        public static void Set<T>(string key, T value) 
            => SerializationLogic.Set(key, value);

        /// <summary>
        /// Load some data, identified by key
        /// </summary>
        /// <typeparam name="T">The type to load the data as</typeparam>
        /// <param name="key">The key the data is identified with</param>
        /// <param name="defaultValue">If there was no data saved with the given key, save and return this</param>
        /// <returns>The loaded value</returns>
        public static T Get<T>(string key, T defaultValue) 
            => SerializationLogic.Get(key, defaultValue);

        /// <summary>
        /// Check if saved data exists identified by this key
        /// </summary>
        public static bool KeyExists(string key)
            => SerializationLogic.KeyExists(key);

        /// <summary>
        /// Delete the data saved at this key, if there is any
        /// </summary>
        public static void DeleteKey(string key)
            => SerializationLogic.DeleteKey(key);

        /// <summary>
        /// Delete all the saved data in GameValues. Be very very careful with this!
        /// </summary>
        public static void DeleteAll(string key)
            => SerializationLogic.DeleteAll();
    }
}