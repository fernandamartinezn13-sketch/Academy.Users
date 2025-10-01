namespace Academy.Users.Domain.Common.EntitiesBase;

public interface IAuditable
{

    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }

}
