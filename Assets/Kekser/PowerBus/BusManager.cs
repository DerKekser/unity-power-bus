namespace Kekser.PowerBus
{
    public class BusManager
    {
        public static BusManager GlobalInstance { get; } = new BusManager();
        
        public void RegisterBus<T>(IBus<T> bus, T initialValue) where T : class
        {
            BusValueHolder<T>.RegisterBus(this, bus, initialValue);
        }
        
        public void UnregisterBus<T>(IBus<T> bus) where T : class
        {
            BusValueHolder<T>.UnregisterBus(this, bus);
        }
        
        public T GetBusValue<T>(IBus<T> bus) where T : class
        {
            return BusValueHolder<T>.GetValue(this);
        }
        
        public void SetBusValue<T>(IBus<T> bus, T value) where T : class
        {
            BusValueHolder<T>.SetValue(this, value);
        }
    }
}