namespace LojaDiversidadeComuniki.Domain.Model.Order
{
    public class Order
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
