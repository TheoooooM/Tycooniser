namespace Services.Interfaces
{
    public interface IService
    {
        void SetServiceState(bool state);
        bool SwitchServiceState();
    }
}