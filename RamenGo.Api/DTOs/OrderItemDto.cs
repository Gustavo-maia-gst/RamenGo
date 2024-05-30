namespace RamenGo.Api.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string ImageActive { get; set; }
        public string ImageInactive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
