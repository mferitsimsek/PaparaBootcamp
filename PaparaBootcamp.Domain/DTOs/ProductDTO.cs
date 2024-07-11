using PaparaBootcamp.Domain.Entities;

namespace PaparaBootcamp.Domain.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public static ProductDTO FromProduct(ProductEntity product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate

            };
        }
    }
}
