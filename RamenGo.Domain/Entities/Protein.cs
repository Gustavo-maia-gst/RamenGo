namespace RamenGo.Domain.Entities
{
    public class Protein : OrderItem
    {
        public Protein(int id,
                     string imageActive,
                     string imageInactive,
                     string name,
                     string description,
                     double price) : base(id, imageActive, imageInactive, name, description, price)
        {
        }
    }
}
