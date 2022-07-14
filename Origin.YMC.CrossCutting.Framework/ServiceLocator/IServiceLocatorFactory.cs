using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    public interface IServiceLocatorFactory
    {
        IServiceLocator Create();
    }
}
