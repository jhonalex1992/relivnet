using relivnet.domain.models;
using System.Linq.Expressions;

namespace relivnet.domain.repositories
{
    public interface IGenericDataRepository<T> where T : class
    {

        Task<PagedCollection<T>> GetPagin(int offset, int limit, params Expression<Func<T, object>>[] navigationProperties);

        IList<TV> GetAllWhereCustomSync<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties);
        Task<PagedCollection<TV>> GetPaginCustom<TV>(Expression<Func<T, TV>> expression, int offset, int limit,
            params Expression<Func<T, object>>[] navigationProperties);

        Task<PagedCollection<TV>> GetPaginCustomWhereOrder<TV>(Expression<Func<T, TV>> expression,
            Expression<Func<T, bool>> where, int offset, int limit,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] navigationProperties);

        PagedCollection<TV> GetPaginCustomWhereOrderSync<TV>(Expression<Func<T, TV>> expression,
            Expression<Func<T, bool>> where, int offset, int limit,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] navigationProperties);
        Task<PagedCollection<T>> GetPaginWhere(Expression<Func<T, bool>> where, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties);
        Task<PagedCollection<T>> GetPaginWhereOrder(Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties);

        PagedCollection<T> GetPaginWhereOrderSync(Expression<Func<T, bool>> where, int offset, int limit,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] navigationProperties);
        PagedCollection<T> GetPaginWhereSync(Expression<Func<T, bool>> where, int offset, int limit, List<string> navigationProperties);
        Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetAllSync(params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<T>> GetAllWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetAllWhereSync(Expression<Func<T, bool>> where, List<string> navigationProperties);
        Task<IList<T>> GetAllWhereOrder(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties);
        //Task<IList<T>> GetAllWhereOrderBy(Expression<Func<T, bool>> where, string[] parameterOrder, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<int> Add(params T[] items);
        int AddRangeSync(ICollection<T> items);
        int AddSync(params T[] items);
        Task<int> Update(params T[] items);
        int UpdateSync(params T[] items);
        int UpdateRangeSync(ICollection<T> items);
        //void UpdateNewContext(params T[] items);
        Task<int> Remove(params T[] items);
        int RemoveSync(params T[] items);
        int RemoveRangeSync(ICollection<T> items);
        T FirstOrDefaultSync(Expression<Func<T, bool>> where);
        T FirstOrDefaultSync(Expression<Func<T, bool>> where, List<string> navigationProperties);
        T SingleSync(Expression<Func<T, bool>> where, string entityMessage, params Expression<Func<T, object>>[] navigationProperties);
        T SingleSync(Expression<Func<T, bool>> where, string entityMessage);
        T SingleSync(Expression<Func<T, bool>> where);
        List<T> GetAllWhereSync(Expression<Func<T, bool>> where);
        int Count(Expression<Func<T, bool>> predicates);
        int UpdateSync(ICollection<T> items);
        bool AnySync(Expression<Func<T, bool>> where);
    }
}
