using System.Collections.Generic;
using System.Linq;

namespace HackThePlanetApp.Data.Repositories.Contracts
{
    public interface IDapperRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();        
    }
}