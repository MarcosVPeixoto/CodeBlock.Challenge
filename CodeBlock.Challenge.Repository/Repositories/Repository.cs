using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.IRepository.Repositories;
using CodeBlock.Challenge.Repository.Data;

namespace CodeBlock.Challenge.Repository.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        protected Repository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void Add(T entity)
        {
            _context.AddAsync(entity); 
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
