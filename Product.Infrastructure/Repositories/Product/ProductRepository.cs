using Product.Infrastructure.Context;
using Product.Model.Models;

namespace Product.Infrastructure.Repositories.Product
{
    public class ProductRepository<TEntity> : IProductRepository<TEntity>
        where TEntity : Products, new()
    {
        private readonly ProductContext productContext;

        public ProductRepository(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        public int Add(TEntity entity)
        {
            productContext.Set<Products>().Add(entity);
            return productContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var d = GetById(id);
            productContext.Set<TEntity>().Remove(d);
            return productContext.SaveChanges();
        }

        public IQueryable<TEntity> Get()
        {
            return productContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return productContext.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public int Update(TEntity entity)
        {
            return (int)(productContext.Update(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
        }
    }
}
