using System.Collections.Generic;
using HackThePlanetApp.Data.Models;

namespace HackThePlanetApp.Data.Repositories.Contracts
{
    public interface IEquipmentRepository : IDapperRepository<Equipment>
    {
        Equipment GetById(int id);
    }
}