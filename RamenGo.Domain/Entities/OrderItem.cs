namespace RamenGo.Domain.Entities
{
    public abstract class OrderItem
    {
        public OrderItem(int id, string imageActive, string imageInactive, string name, string description, double price)
        {
            Id = id;
            ImageActive = imageActive;
            ImageInactive = imageInactive;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Id { get; private set; }
        public string ImageActive { get; private set; }
        public string ImageInactive { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
    }
}
