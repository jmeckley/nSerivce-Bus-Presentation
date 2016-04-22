namespace Cms
{
    public interface IProductCatalog
    {
        Product Get(int id);
    }

    public class ProductCatalog 
        : IProductCatalog
    {
        public Product Get(int id)
        {
            return new Product
            {
                Id = id,
                Description = string.Format("Item {0:000}", id),
                Price = 50M
            };
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}