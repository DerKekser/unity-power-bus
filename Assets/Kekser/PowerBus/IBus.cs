namespace Kekser.PowerBus
{
    public interface IBus<T> where T : class
    {
        T Value { get; set; }
        event System.Action<T> OnChange;
        void InvokeOnChange();
    }
}