namespace Kekser.PowerBus
{
    public interface IBus<T> where T : class
    {
        void Trigger(T value);
        event BusEvent<T> OnChange;
        void InvokeOnChange(T value);
    }
}