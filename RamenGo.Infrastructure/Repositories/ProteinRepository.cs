using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;

namespace RamenGo.Infrastructure.Repositories
{
    public class ProteinRepository : IProteinRepository
    {
        private readonly RamenGoDbContext _dbContext;

        public ProteinRepository(RamenGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Protein> GetAll()
        {
            return _dbContext.Proteins.ToList();
        }

        public void Delete(Protein entity)
        {
            _dbContext.Proteins.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Protein Get(int key)
        {
            Protein? protein = _dbContext.Proteins.Find(key);
            return protein
                ?? throw new KeyNotFoundException($"The protein with id = {key} not found");
        }

        public void Insert(Protein entity)
        {
            _dbContext.Proteins.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Protein entity)
        {
            _dbContext.Proteins.Update(entity);
            _dbContext.SaveChanges();
        }

        public bool Contains(Protein protein)
        {
            return _dbContext.Proteins.Contains(protein);
        }
    }
}
