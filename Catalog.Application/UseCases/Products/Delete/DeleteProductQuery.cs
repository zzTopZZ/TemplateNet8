using Catalog.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.UseCases.Products.Delete
{
    public class DeleteProductQuery (int id) : IRequest
    {
        public int Id { get; set; } = id;   
    }
}
