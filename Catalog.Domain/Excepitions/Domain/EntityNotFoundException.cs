using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Excepitions.Domain
{
    public class EntityNotFoundException (string msg) : DomainException(msg);
}
