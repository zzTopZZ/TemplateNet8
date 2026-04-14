using Catalog.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.UseCases.Products.Create
{
    public class CreateProductQuery : IRequest
    {
        [Required]
        public string? Name { get; set; }
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int? CategoryId { get; set; }
    }
}
