using System.Linq.Expressions;

namespace QuizItEasy.Domain.Common;

public abstract class Specification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; protected set; }
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }

    public Specification<T> AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
        return this;
    }

    public Specification<T> AddOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
    {
        OrderBy = orderBy;
        return this;
    }

    public Specification<T> ApplyPaging(int pageNumber, int pageSize)
    {
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        return this;
    }
}

