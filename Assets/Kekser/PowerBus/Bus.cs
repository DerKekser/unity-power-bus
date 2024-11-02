using System;

namespace Kekser.PowerBus
{
    public class Bus
    {
        public static void Trigger<T>(T value, BusManager manager = null) where T : class => (manager ?? BusManager.GlobalInstance).TriggerBus(value);
    }
    
    public class Bus<T> : IBus<T> where T : class
    {
        private BusManager _manager;
        private BusEvent<T> _onChange;
        
        public Bus(BusManager manager = null)
        {
            _manager = manager ?? BusManager.GlobalInstance;
            _manager.RegisterBus(this);
        }
        
        public Bus(BusEvent<T> onChange, BusManager manager = null)
        {
            _manager = manager ?? BusManager.GlobalInstance;
            _manager.RegisterBus(this);
            _onChange = onChange;
            OnChange += _onChange;
        }
        
        ~Bus()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            if (_onChange != null)
                OnChange -= _onChange;
            _manager.UnregisterBus(this);
        }
        
        public void Trigger(T value) => _manager.TriggerBus(value);

        public event BusEvent<T> OnChange;
        
        public void InvokeOnChange(T value) => OnChange?.Invoke(value);
    }
}
