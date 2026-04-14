using Catalog.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Products.GetById
{
    public class GetByIdProductQuery (int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}
