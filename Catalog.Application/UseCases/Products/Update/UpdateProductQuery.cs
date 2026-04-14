using Catalog.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.UseCases.Products.Update
{
    public class UpdateProductQuery : IRequest
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal? Price { get; set; }
    }
}
