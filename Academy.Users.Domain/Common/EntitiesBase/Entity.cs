namespace Academy.Users.Domain.Common.EntitiesBase;

public abstract class Entity<TEntity>
{
    public TEntity Id { get; set; }

    protected Entity(TEntity id)
    {
        Id = id;
    }
}
