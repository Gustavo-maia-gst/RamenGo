namespace RamenGo.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int BrothId { get; set; }
        public int ProteinId { get; set; }

        public Order(int id, Broth broth, Protein protein)
        {
            Id = id;
            BrothId = broth.Id;
            ProteinId = protein.Id;
        }

        protected Order() { }       // Construtor vazio para o ORM
    }
}
