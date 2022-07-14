using Origin.YMC.Business.Entities.Domain.Log.Enums;


namespace Origin.YMC.Business.Entities.Domain.Log
{
    public class Log : EntityBase
    {

        public string Message { get; set; }
        public LogType LogType { get; set; }
        public string MethodName { get; set; }
    }
}
