using System.Collections.Generic;
using UnityEngine;

namespace Kekser.PowerBus
{
    public static class BusHolder<T> where T : class
    {
        private static Dictionary<BusManager, List<IBus<T>>> _buses = new Dictionary<BusManager, List<IBus<T>>>();
        
        public static void RegisterBus(BusManager manager, IBus<T> bus)
        {
            if (_buses.ContainsKey(manager))
                _buses[manager].Add(bus);
            else
                _buses[manager] = new List<IBus<T>> { bus };
        }
        
        public static void UnregisterBus(BusManager manager, IBus<T> bus)
        {
            if (_buses.ContainsKey(manager))
                _buses[manager].Remove(bus);
        }
        
        public static void TriggerBus(BusManager manager, T value)
        {
            if (!_buses.TryGetValue(manager, out List<IBus<T>> buses)) 
                return;
            for (int i = buses.Count - 1; i >= 0; i--)
                buses[i].InvokeOnChange(value);
        }
    }
}