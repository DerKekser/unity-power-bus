using System.Collections.Generic;
using UnityEngine;

namespace Kekser.PowerBus
{
    public static class BusValueHolder<T> where T : class
    {
        private static Dictionary<BusManager, T> _values = new Dictionary<BusManager, T>();
        private static Dictionary<BusManager, List<IBus<T>>> _buses = new Dictionary<BusManager, List<IBus<T>>>();
        
        public static void RegisterBus(BusManager manager, IBus<T> bus, T initialValue)
        {
            if (_buses.ContainsKey(manager))
                _buses[manager].Add(bus);
            else
                _buses[manager] = new List<IBus<T>> { bus };
            if (_buses[manager].Count == 1)
                SetValue(manager, initialValue);
        }
        
        public static void UnregisterBus(BusManager manager, IBus<T> bus)
        {
            if (_buses.ContainsKey(manager))
                _buses[manager].Remove(bus);
        }
        
        public static T GetValue(BusManager manager)
        {
            return _values.TryGetValue(manager, out T value) ? value : null;
        }
        
        public static void SetValue(BusManager manager, T value)
        {
            _values[manager] = value;

            if (!_buses.TryGetValue(manager, out List<IBus<T>> buses)) 
                return;
            for (int i = buses.Count - 1; i >= 0; i--)
                buses[i].InvokeOnChange();
        }
    }
}