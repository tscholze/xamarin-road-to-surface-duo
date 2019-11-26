using System.Collections.Generic;
using System.Threading.Tasks;

namespace rTsd.Services
{
    /// <summary>
    /// Generic readonly element service.
    /// </summary>
    /// <typeparam name="T">Typ of elements.</typeparam>
    public interface IElementService<T>
    {
        /// <summary>
        /// Gets all elements async.
        /// </summary>
        /// <returns>Get all feed items</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets a single element found by it's id.
        /// </summary>
        /// <param name="id">Id to look for.</param>
        /// <returns>Found element</returns>
        T GetById(string id);
    }
}
