namespace RamenGo.Domain.Entities
{
    public class Broth : OrderItem
    {
        public Broth(int id,
                     string imageActive,
                     string imageInactive,
                     string name,
                     string description,
                     double price) : base(id, imageActive, imageInactive, name, description, price)
        {
        }
    }
}
