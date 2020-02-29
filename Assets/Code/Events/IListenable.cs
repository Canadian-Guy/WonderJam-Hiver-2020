public interface IListenable
{
    void RegisterListener(IListener listener);
    void UnregisterListener(IListener listener);
}
