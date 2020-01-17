using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public static class Data
    {
        public static void Set<T>(string key, T value)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            SerializationLogicDesktop.Set(key, value);
#else
            SerializationLogicOther.Set(key, value);
#endif
        }

        public static T Get<T>(string key)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return SerializationLogicDesktop.Get<T>(key);
#else
            return SerializationLogicOther.Get<T>(key);
#endif
        }
    }
}