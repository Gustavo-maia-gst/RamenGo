using RamenGo.Domain.Entities;

namespace RamenGo.Domain.Repositories
{
    public interface IProteinRepository : IBaseRepository<Protein, int>
    {
        public List<Protein> GetAll();
        public bool Contains(Protein protein);
    }
}
