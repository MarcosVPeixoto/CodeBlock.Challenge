namespace CodeBlock.Challenge.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DataCriacao{ get; set; }
        public BaseEntity()
        {
            DataCriacao = DateTime.Now;
        }
    }
}
