using System;

namespace Kekser.PowerBus
{
    public class Bus<T> : IBus<T> where T : class
    {
        private BusManager _manager;
        private Action<T> _onChange;
        
        public Bus(T initialValue = null, BusManager manager = null)
        {
            _manager = manager ?? BusManager.GlobalInstance;
            _manager.RegisterBus(this, initialValue);
        }
        
        public Bus(Action<T> onChange, T initialValue = null, BusManager manager = null)
        {
            _manager = manager ?? BusManager.GlobalInstance;
            _manager.RegisterBus(this, initialValue);
            _onChange = onChange;
            OnChange += _onChange;
        }
        
        ~Bus()
        {
            if (_onChange != null)
                OnChange -= _onChange;
            _manager.UnregisterBus(this);
        }

        public event Action<T> OnChange;
        
        public void InvokeOnChange() => OnChange?.Invoke(Value);

        public T Value
        {
            get => _manager.GetBusValue(this);
            set => _manager.SetBusValue(this, value);
        }
        
        public static implicit operator T(Bus<T> bus) => bus.Value;
    }
}
