using ApiProject.Data;
using Microsoft.EntityFrameworkCore;
using ApiProject.Data;

namespace ApiProject.Repositories
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        private  SchoolContext _context;

        public GenericRepo(SchoolContext context)
        {
            _context = context;
        }

        // Get all records
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        // Get by Id (int)
        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        // Add new record
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        // Update existing record
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        // Delete by Id
        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
