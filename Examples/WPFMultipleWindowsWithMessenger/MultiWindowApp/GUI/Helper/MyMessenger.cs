using System;
using System.Collections.Generic;

namespace GUI.Helper
{
    public class MyMessenger
    {
        private static Dictionary<Type, Action<object>> registrants = 
            new Dictionary<Type, Action<object>>();

        public static void Register<T>(Action<object> action) where T : class
        {
            registrants.Add(typeof(T), action);
        }

        public static void Send<T>(T obj) where T : class
        {
            foreach (var item in registrants)
            {
                if (item.Key == typeof(T))
                {
                    item.Value(obj);
                    //break; // remove this to allow multiple recipients
                }
            }
        }
    }
}
