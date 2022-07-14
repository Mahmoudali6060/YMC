using Origin.YMC.Business.Entities.Domain.Log;


namespace Origin.YMC.Business.Components.Interfaces
{
    public interface ILogComponent
    {
        void Add(Log promotion);
        void LogInfo(string Message, string MethodName);
        void LogError(string Message, string MethodName);
    }
}
