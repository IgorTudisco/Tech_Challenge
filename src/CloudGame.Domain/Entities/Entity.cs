namespace CloudGame.Domain.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public DateTime? UpdateAt { get; protected set; }

    public Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdateAt()
    {
        UpdateAt = DateTime.UtcNow;
    }
}
