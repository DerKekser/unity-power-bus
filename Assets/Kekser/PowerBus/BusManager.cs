namespace Kekser.PowerBus
{
    public class BusManager
    {
        public static BusManager GlobalInstance { get; } = new BusManager();
        
        public void RegisterBus<T>(IBus<T> bus) where T : class
        {
            BusHolder<T>.RegisterBus(this, bus);
        }
        
        public void UnregisterBus<T>(IBus<T> bus) where T : class
        {
            BusHolder<T>.UnregisterBus(this, bus);
        }
        
        public void TriggerBus<T>(T value) where T : class
        {
            BusHolder<T>.TriggerBus(this, value);
        }
    }
}