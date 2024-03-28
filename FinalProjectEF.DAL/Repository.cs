using FinalProjectEF.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectEF.DAL
{
    public class Repository<TEntity> where TEntity : class
    {
        private readonly Context _context;

        public Repository()
        {
            _context = new Context();
        }
        /*public Repository(DbContextOptions options)
        {
            _context = new Context(options);
        }*/

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public List<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetById(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            yield return entity;
        }

        public TEntity? GetById<TEntity>(int id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);

            _context.Set<TEntity>().Remove(entity);

            _context.SaveChanges();
        }

        public void Update(int id, TEntity entity)
        {
            var existingEntity = _context.Set<TEntity>().Find(id);

            _context.Entry(existingEntity).CurrentValues
                .SetValues(entity);

            _context.SaveChanges();
        }

        public void Delete(object standing)
        {
            _context.Remove(standing);
            _context.SaveChanges();
        }
    }
}
