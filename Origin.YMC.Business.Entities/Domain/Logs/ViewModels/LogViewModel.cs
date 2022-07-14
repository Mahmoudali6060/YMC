using System.Collections.Generic;
namespace Origin.YMC.Business.Entities.Domain.Log.ViewModels
{
    public class LogCollectionViewModel
    {
        public List<LogViewModel> Logs { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
    }
    public class LogViewModel
    {
        public string Message { get; set; }
        public string CreationDate { get; set; }
        public string UserName { get; set; }
        public string LogType { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
    }
}
