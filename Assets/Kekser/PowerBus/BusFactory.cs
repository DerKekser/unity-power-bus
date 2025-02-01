using System.Collections.Generic;
using JetBrains.Annotations;

namespace Kekser.PowerBus
{
    public static class BusFactory<T> where T : class
    {
        private static Dictionary<BusManager, IBus<T>> _buses = new Dictionary<BusManager, IBus<T>>();
        
        public static IBus<T> CreateBus(BusManager manager = null)
        {
            manager ??= BusManager.GlobalInstance;
            if (_buses.TryGetValue(manager, out IBus<T> bus))
                return bus;
            bus = new Bus<T>(manager);
            _buses[manager] = bus;
            return bus;
        }
        
        public static void DisposeBus(BusManager manager = null)
        {
            manager ??= BusManager.GlobalInstance;
            if (!_buses.TryGetValue(manager, out IBus<T> bus)) return;
            manager.UnregisterBus(bus);
            _buses.Remove(manager);
        }
    }
}