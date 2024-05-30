using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;

namespace RamenGo.Infrastructure.Repositories
{
    public class BrothRepository : IBrothRepository
    {
        private readonly RamenGoDbContext _dbContext;

        public BrothRepository(RamenGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Broth> GetAll()
        {
            return _dbContext.Broths.ToList();
        }

        public void Delete(Broth entity)
        {
            _dbContext.Broths.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Broth Get(int key)
        {
            Broth? broth = _dbContext.Broths.Find(key);
            return broth
                ?? throw new KeyNotFoundException($"The broth with id = {key} not found");
        }

        public void Insert(Broth entity)
        {
            _dbContext.Broths.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Broth entity)
        {
            _dbContext.Broths.Update(entity);
            _dbContext.SaveChanges();
        }

        public bool Contains(Broth broth)
        {
            return _dbContext.Broths.Contains(broth);
        }
    }
}
