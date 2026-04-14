using Catalog.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Categories
{
    public class CategoryResponse : IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
