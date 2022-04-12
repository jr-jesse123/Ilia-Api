namespace ILIA.SimpleStore.Domain;

public abstract class EntityBase
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get;  }
}

