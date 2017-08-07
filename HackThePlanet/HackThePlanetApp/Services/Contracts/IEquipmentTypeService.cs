using System.Collections.Generic;
using HackThePlanetApp.Data.Models;

namespace HackThePlanetApp.Services.Contracts
{
    public interface IEquipmentTypeService
    {
        IEnumerable<EquipmentType> GetAll();
    }
}