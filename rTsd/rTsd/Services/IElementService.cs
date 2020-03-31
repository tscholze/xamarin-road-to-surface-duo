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
        /// Gets all elements.
        /// </summary>
        /// <returns>Get all feed items</returns>
        List<T> GetAll(bool forceReload = false);
    }
}
