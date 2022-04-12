using ILIA.SimpleStore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected SimpleStoreContext Context;
    protected DbSet<T> Entities;

    public Repository(SimpleStoreContext context)
    {
        Context = context;
        Entities = context.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        await Entities.AddAsync(entity);
        return entity;
    }

    public async Task<int> Commit()
    {
        return await Context.SaveChangesAsync();
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await Entities.FindAsync(id);

        if (entity is null)
        {
            return false;
        }
        else
        {
            Entities.Remove(entity);
            return true;
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Entities.ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await Entities.FindAsync(id);
    }

    public void Update(T entity)
    {
        Entities.Update(entity);
    }

}
