using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentasAhorro.Repository.Interface
{
    public interface ICrudRepository<T> where T : class
    {
        /// <summary>
        /// Insertar una entidad en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Actualizar una entidad de la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Eliminar una entidad de la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Obtiene una entidad de la base de datos, en base a una condición
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> condition);

        /// <summary>
        /// Obtiene determinados campos de una entidad, en base a una condición
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="condition"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        Task<TType> GetAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection) where TType : class;

        /// <summary>
        /// Obtiene una lista de todos los elementos de la entidad
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// Obtiene una lista de los elementos una entidad, en base a una condición
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> condition);

        /// <summary>
        /// Obtiene una lista de determinados campos de una entidad
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="selection"></param>
        /// <returns></returns>
        Task<List<TType>> GetListAsync<TType>(Expression<Func<T, TType>> selection) where TType : class;

        /// <summary>
        /// Obtiene una lista de determinados campos de una entidad, en base a una condición
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="condition"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        Task<List<TType>> GetListAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection) where TType : class;

        /// <summary>
        /// Obtiene una lista de elementos, utilizando paginación
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene una lista de elementos de una entidad en base a una condición, utilizando paginación
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetPagedReponseAsync(Expression<Func<T, bool>> condition, int pageNumber, int pageSize);

        /// <summary>
        /// Obtiene una lista de determinados campos de una entidad, en base a una condición, utilizando paginación
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="condition"></param>
        /// <param name="selection"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ICollection<TType>> GetPagedReponseAsync<TType>(Expression<Func<T, bool>> condition, Expression<Func<T, TType>> selection, int pageNumber, int pageSize) where TType : class;

        /// <summary>
        /// Obtiene el total de elementos de una entidad
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Obtiene el total de elementos de una entidad en base a una condición
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> condition);
    }
}