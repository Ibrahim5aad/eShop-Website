using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(params object[] id);

        IQueryable<TEntity> GetAll();

        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

    }
}