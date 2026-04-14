using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Products
{
    public static class Mapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = new CategoryProductResponse
                {
                    Id = product.CategoryId,
                    Name = product.Category?.Name ?? string.Empty
                }
            };
        }
    }
}
