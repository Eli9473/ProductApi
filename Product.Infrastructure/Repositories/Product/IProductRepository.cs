using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories.Product
{
    public interface IProductRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        TEntity GetById(int id);

        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);
    }
}
