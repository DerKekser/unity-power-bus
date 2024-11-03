namespace Kekser.PowerBus
{
    public interface IBus<T> where T : class
    {
        void Trigger(T value);
        event BusEvent<T> On;
        event BusEvent<T> Listener; 
        void InvokeOnChange(T value);
    }
}