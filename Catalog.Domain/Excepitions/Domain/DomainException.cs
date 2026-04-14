using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Excepitions.Domain
{
    public abstract class DomainException (string msg) : Exception(msg);
}
