using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; } =null!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
