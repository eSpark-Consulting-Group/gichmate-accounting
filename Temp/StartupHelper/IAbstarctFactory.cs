namespace AccountsPRO.StartupHelper
{
    public interface IAbstarctFactory<T>
    {
        T Create();
    }
}