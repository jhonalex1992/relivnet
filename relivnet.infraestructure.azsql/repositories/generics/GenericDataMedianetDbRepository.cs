using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using relivnet.domain.models;
using relivnet.domain.repositories;
using relivnet.infraestructure.azsql.contexts;
using relivnet.infraestructure.azsql.exceptions;

namespace relivnet.infraestructure.azsql.repositories.generics
{
    public abstract class GenericDataMedianetDbRepository<T> : IGenericDataRepository<T> where T : class
    {
        public RelivnetDbContext Context { get; set; }
        //private readonly IConfiguration _configuration;

        public GenericDataMedianetDbRepository(RelivnetDbContext contex)
        {
            Context = contex;
        }


        public virtual async Task<PagedCollection<T>> GetPagin(int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync();
            var items = await dbQuery
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();
            return new PagedCollection<T>()
            {
                Items = items,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual IList<TV> GetAllWhereCustomSync<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var noreturn = dbQuery.Where(@where).AsEnumerable().Select(expression.Compile()).ToList();
            return noreturn;
        }

        public virtual async Task<PagedCollection<TV>> GetPaginCustom<TV>(Expression<Func<T, TV>> expression, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync();
            var items = await dbQuery
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<TV>> GetPaginCustomWhereOrder<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await dbQuery.CountAsync(where);

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);


            var items = await dbQuery
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual PagedCollection<TV> GetPaginCustomWhereOrderSync<TV>(Expression<Func<T, TV>> expression, Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = dbQuery.Count(where);

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);


            var items = dbQuery
                .Skip(offset)
                .Take(limit)
                .ToArray();
            var data = items.Select(expression.Compile()).ToArray();
            return new PagedCollection<TV>()
            {
                Items = data,
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<T>> GetPaginWhere(Expression<Func<T, bool>> where, int offset, int limit, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var count = await Task.FromResult(dbQuery.Count(where));

            var items = await Task.FromResult(dbQuery.Where(where).Skip(offset)
                .Take(limit));
            return new PagedCollection<T>()
            {
                Items = items.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        public virtual async Task<PagedCollection<T>> GetPaginWhereOrder(Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            var count = await Task.FromResult(dbQuery.Count(where));
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);



            var dbResult = await Task.FromResult(dbQuery
                .Skip(offset)
                .Take(limit));
            return new PagedCollection<T>()
            {
                Items = dbResult.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };

        }

        public virtual PagedCollection<T> GetPaginWhereOrderSync(Expression<Func<T, bool>> where, int offset, int limit, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            var count = dbQuery.Count(where);
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }

            dbQuery = dbQuery.Where(where).AsQueryable();

            if (orderBy != null)
                dbQuery = orderBy(dbQuery);

            var dbResult = dbQuery
                .Skip(offset)
                .Take(limit);
            return new PagedCollection<T>()
            {
                Items = dbResult.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };

        }
        public virtual PagedCollection<T> GetPaginWhereSync(Expression<Func<T, bool>> where, int offset, int limit, List<string> navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (navigationProperties != null)
            {
                foreach (string navigation in navigationProperties)
                {
                    if (!string.IsNullOrEmpty(navigation))
                    {
                        dbQuery = dbQuery.Include(navigation);
                    }
                }
            }
            var count = dbQuery.Count(where);
            var items = dbQuery.Where(where).Skip(offset)
                .Take(limit);
            return new PagedCollection<T>()
            {
                Items = items.ToArray(),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }
        public virtual async Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.ToArrayAsync();
            return retorno;
        }

        public virtual IList<T> GetAllSync(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = dbQuery.ToList();
            return retorno;
        }

        public virtual List<T> GetAllWhereSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            var retorno = dbQuery.Where(where).ToList();
            return retorno;
        }

        public virtual async Task<IList<T>> GetAllOrder(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (orderBy != null)
                dbQuery = orderBy(dbQuery);
            var retorno = await dbQuery.ToArrayAsync();
            return retorno;
        }
        public virtual async Task<IList<T>> GetAllWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }

        public IList<T> GetAllWhereSync(Expression<Func<T, bool>> where, List<string> navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = dbQuery.Where(where).ToList();
            return retorno;
        }

        public virtual async Task<IList<T>> GetAllWhereOrder(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (orderBy != null)
                dbQuery = orderBy(dbQuery);
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }

        public virtual int Count(Expression<Func<T, bool>> predicates)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            return dbQuery.Count(predicates); ;
        }

        public virtual async Task<IList<T>> GetAllWhereOrderBy(Expression<Func<T, bool>> where, string[] parameterOrder, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            if (parameterOrder != null)
            {
                dbQuery = dbQuery.Where(where).AsQueryable();
                IOrderedQueryable<T> query = null;
                if (!string.IsNullOrEmpty(parameterOrder[0]))
                {
                    var propertyInfo = typeof(T).GetProperty(parameterOrder[0]);
                    query = dbQuery.OrderBy(x => propertyInfo.GetValue(x, null));
                    for (int i = 1; i < parameterOrder.Length; i++)
                    {
                        var property = typeof(T).GetProperty(parameterOrder[i]);
                        query = query.ThenBy(x => property.GetValue(x, null));
                    }
                }
                var result = await Task.FromResult(query.ToList());
                return result;

            }
            var retorno = await dbQuery.Where(where).ToListAsync();
            return retorno;
        }


        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (var navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var retorno = await dbQuery.FirstOrDefaultAsync(where);
            return retorno;
        }


        public virtual T FirstOrDefaultSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            T retorno = dbQuery.FirstOrDefault(where);
            return retorno;
        }

        public virtual T FirstOrDefaultSync(Expression<Func<T, bool>> where, List<string> navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (navigationProperties != null)
            {
                foreach (string navigation in navigationProperties)
                {
                    if (!string.IsNullOrEmpty(navigation))
                    {
                        dbQuery = dbQuery.Include(navigation);
                    }
                }
            }
            T result = dbQuery.FirstOrDefault(where);
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where, string entityMessage)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException(entityMessage);
            }
            return result;
        }

        public virtual T SingleSync(Expression<Func<T, bool>> where, string entityMessage, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            foreach (Expression<Func<T, object>> navigation in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigation);
            }
            var result = dbQuery.FirstOrDefault(where);
            if (result == null)
            {
                throw new NotFoundException(entityMessage);
            }
            return result;
        }

        public virtual async Task<int> Add(params T[] items)
        {
            var result = 0;

            try
            {
                foreach (var item in items)
                {
                    Context.Entry(item).State = EntityState.Added;
                }
                result = await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var err = ex.Message.ToString();
            }

            return result;
        }

        public virtual int AddRangeSync(ICollection<T> items)
        {
            this.Context.AddRange(items);
            return this.Context.SaveChanges();
        }

        public virtual int RemoveRangeSync(ICollection<T> items)
        {
            this.Context.RemoveRange(items);
            return this.Context.SaveChanges();
        }


        public int AddSync(params T[] items)
        {
            foreach (T item in items)
            {
                Context.Entry(item).State = EntityState.Added;
            }
            var result = Context.SaveChanges();
            return result;
        }

        public virtual async Task<int> Update(params T[] items)
        {

            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Modified;
            }
            var result = await Context.SaveChangesAsync();
            return result;

        }

        public virtual int UpdateSync(ICollection<T> items)
        {
            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Modified;
            }
            int result = Context.SaveChanges();
            return result;
        }


        //public void UpdateNewContext(params T[] items)
        //{
        //    using (MedianetDbContext context = new MedianetDbContext())
        //    {
        //        context.UpdateRange(items);
        //        context.SaveChanges();
        //    }
        //}

        public virtual int UpdateSync(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Modified;
            }
            int retorno = Context.SaveChanges();
            return retorno;
        }

        public virtual int UpdateRangeSync(ICollection<T> items)
        {
            this.Context.UpdateRange(items);
            int retorno = Context.SaveChanges();
            return retorno;
        }

        public virtual async Task<int> Remove(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }
            var retorno = await Context.SaveChangesAsync();
            return retorno;
        }

        public virtual int RemoveSync(params T[] items)
        {
            foreach (var item in items)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }
            var retorno = Context.SaveChanges();
            return retorno;
        }

        public virtual bool AnySync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            return dbQuery.Any(where);
        }


    }
}
