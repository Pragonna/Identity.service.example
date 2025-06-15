namespace Shared.DataAccess.Common;

public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Entity(Guid id)
    {
        this.Id = id;
    }
}