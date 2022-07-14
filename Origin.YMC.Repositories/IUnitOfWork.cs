using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        int SaveChanges(bool DontGenerateId);
    }
}
