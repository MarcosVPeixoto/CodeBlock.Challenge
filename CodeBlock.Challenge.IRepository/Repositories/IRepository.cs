using CodeBlock.Challenge.Domain.Entities;

namespace CodeBlock.Challenge.IRepository.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        Task SaveContext();
    }
}
