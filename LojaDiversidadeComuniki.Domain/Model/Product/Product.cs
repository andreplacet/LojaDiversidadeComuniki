namespace LojaDiversidadeComuniki.Domain.Model.Products
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }

    }

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
