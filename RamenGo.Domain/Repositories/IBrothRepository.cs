using RamenGo.Domain.Entities;

namespace RamenGo.Domain.Repositories
{
    public interface IBrothRepository : IBaseRepository<Broth, int>
    {
        public List<Broth> GetAll();
        public bool Contains(Broth broth);
    }
}
