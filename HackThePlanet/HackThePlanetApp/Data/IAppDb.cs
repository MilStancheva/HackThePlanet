using System.Data;

namespace HackThePlanetApp.Data
{
    public interface IAppDb
    {
        IDbConnection DbConnection { get; }

        void Dispose();
    }
}