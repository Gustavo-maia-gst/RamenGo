using RamenGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamenGo.Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order, int>
    {
    }
}
