using CuentasAhorro.Services.Wrappers;

namespace CuentasAhorro.Services.Interface
{
    /// <summary>
    /// Implementación básica del CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Insertar un registro en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Response<T>> InsertAsync(T entity);

        /// <summary>
        /// Obtiene un registro por medio de su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<T>> GetByIdAsync(object id);

        /// <summary>
        /// Obtiene una lista de registros
        /// </summary>
        /// <returns></returns>
        Task<Response<List<T>>> GetListAsync(object id);

        /// <summary>
        /// Actualiza un registro de la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Response<bool>> UpdateAsync(T entity);

        /// <summary>
        /// Elimina un registro de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response<bool>> DeleteAsync(object id);
    }
}