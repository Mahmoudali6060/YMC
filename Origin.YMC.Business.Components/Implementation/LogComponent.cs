using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.Log;
using Origin.YMC.Business.Entities.Domain.Log.Enums;
using Origin.YMC.Repositories;
using System;

namespace Origin.YMC.Business.Components.Implementation
{
    public class LogComponent : ILogComponent
    {
        private readonly IRepository<Log> _logRepository;

        public LogComponent(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;

        }
        public void Add(Log log)
        {
            _logRepository.Add(log);
            _logRepository.UnitOfWork.SaveChanges();
        }

        public void LogError(string Message, string MethodName)
        {
            Add(new Log
            {
                CreationDate = DateTime.Now,
                Message = Message,
                LogType = LogType.Error,
                MethodName = MethodName
            });

        }

        public void LogInfo(string Message, string MethodName)
        {
            Add(new Log
            {
                CreationDate = DateTime.Now,
                Message = Message,
                LogType = LogType.Info,
                MethodName = MethodName
            });

        }
    }
}
