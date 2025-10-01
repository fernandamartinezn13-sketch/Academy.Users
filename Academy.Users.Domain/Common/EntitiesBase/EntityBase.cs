namespace Academy.Users.Domain.Common.EntitiesBase;

public abstract class EntityBase<TId>
{
    public TId Id { get; set; } = default!;
}
