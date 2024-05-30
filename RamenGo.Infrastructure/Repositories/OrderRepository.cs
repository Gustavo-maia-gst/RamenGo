using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;

namespace RamenGo.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RamenGoDbContext _dbContext;

        public OrderRepository(RamenGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Order Get(int key)
        {
            Order? order = _dbContext.Orders.Find(key);
            return order
                ?? throw new KeyNotFoundException($"The order with id = {key} not found");
        }

        public void Insert(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
