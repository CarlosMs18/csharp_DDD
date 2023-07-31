using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync(); //devuelce una lista gwenerica

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);//devoluciond e datos con una condicion en query,
                                                                             //le pasamosp la conficion logica manejado mediante funcituioins, T el valor de entrada y el valor que retornara un bool si es verdadro o falso
                                                                             //esa expression de funcituion que pasaremso a futuro se convertira ern una funcion SQL

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString=null,
                                         bool disableTracking=true ); //2 parametro sera del ordenamiento


        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T,object>>> includes = null,
                                         bool disableTracking = true);//3 parametros para haceer consultas entre tabla osea medianrte join es el parametro include

        Task<T> GetByIdAsync(int id);


        Task<T> AddAsync(T entity);

        Task<T> UpdateAync(T entity);

        Task DeleteAsync(T entity);
    }
}
