using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GreenPuffer.Misc
{
    public static class Storage
    {
        public static void Save(string key, object obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                PlayerPrefs.SetString(key, Convert.ToBase64String(stream.GetBuffer()));
                PlayerPrefs.Save();
            }
        }

        public static T Load<T>(string key)
        {
            var str = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(str))
                return default(T);

            using (var stream = new MemoryStream(Convert.FromBase64String(str)))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        public static T Load<T>(string key, T defaultValue)
        {
            var value = Load<T>(key);
            if (value == null)
            {
                return defaultValue;
            }
            return value;
        }
    }
}
