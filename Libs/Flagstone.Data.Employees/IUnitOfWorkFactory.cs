using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagstone.Data.Employees
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
