using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TModel>
        where TModel : class, IHasId
    {
        Task<IList<TModel>> GetAll();

        Task<IList<TModel>> Find(Expression<Func<TModel, Boolean>> predicate);

        Task<IList<TModel>> Find<TProperty>(Expression<Func<TModel, Boolean>> predicate, Expression<Func<TModel, TProperty>> include);

        Task<TModel> Single(Expression<Func<TModel, Boolean>> predicate);

        Task<TModel> Create(TModel toCreate);

        Task Update(TModel toUpdate);

        Task Update(IList<TModel> toUpdate);

        Task Remove(TModel toRemove);

        Task Remove(IList<TModel> toRemove);
    }
}