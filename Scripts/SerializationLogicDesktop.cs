#if UNITY_EDITOR || UNITY_STANDALONE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUCC;

namespace PersistentData
{
    internal static class SerializationLogicDesktop
    {
        static readonly DataFile File = new DataFile(nameof(PersistentData));

        internal static void Set<T>(string key, T value)
        {
            File.Set(key, value);
        }

        internal static T Get<T>(string key)
        {
            return File.Get<T>(key);
        }
    }

}
#endif