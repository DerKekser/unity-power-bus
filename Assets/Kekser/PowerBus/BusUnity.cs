using System.Collections.Generic;
using UnityEngine;

namespace Kekser.PowerBus
{
    public static class BusGameObject<T> where T : class
    {
        public static Dictionary<int, Bus<T>> Buses = new Dictionary<int, Bus<T>>();
        public static Dictionary<(int, BusManager), Bus<T>> LocalBuses = new Dictionary<(int, BusManager), Bus<T>>();
    }
    
    public static class BusUnity
    {
        private static Dictionary<int, BusManager> _managers = new Dictionary<int, BusManager>();
        
        public static Bus<T> Bus<T>(this GameObject gameObject, BusManager manager = null) where T : class
        {
            if (!BusGameObject<T>.Buses.TryGetValue(gameObject.GetInstanceID(), out Bus<T> bus))
            {
                bus = new Bus<T>(manager);
                BusGameObject<T>.Buses[gameObject.GetInstanceID()] = bus;
            }
            
            return bus;
        }
        
        public static Bus<T> LocalBus<T>(this GameObject gameObject) where T : class
        {
            if (!_managers.TryGetValue(gameObject.GetInstanceID(), out BusManager manager))
            {
                manager = new BusManager();
                _managers[gameObject.GetInstanceID()] = manager;
            }
            
            if (!BusGameObject<T>.LocalBuses.TryGetValue((gameObject.GetInstanceID(), manager), out Bus<T> bus))
            {
                bus = new Bus<T>(manager);
                BusGameObject<T>.LocalBuses[(gameObject.GetInstanceID(), manager)] = bus;
            }
            
            return bus;
        }
        
        public static void DisposeBus<T>(this GameObject gameObject) where T : class
        {
            if (BusGameObject<T>.Buses.TryGetValue(gameObject.GetInstanceID(), out Bus<T> bus))
            {
                bus.Dispose();
                BusGameObject<T>.Buses.Remove(gameObject.GetInstanceID());
            }
        }

        public static void DisposeLocalBus<T>(this GameObject gameObject) where T : class
        {
            if (_managers.TryGetValue(gameObject.GetInstanceID(), out BusManager manager)
                && BusGameObject<T>.LocalBuses.TryGetValue((gameObject.GetInstanceID(), manager), out Bus<T> bus))
            {
                bus.Dispose();
                BusGameObject<T>.LocalBuses.Remove((gameObject.GetInstanceID(), manager));
            }
        }
    }
}