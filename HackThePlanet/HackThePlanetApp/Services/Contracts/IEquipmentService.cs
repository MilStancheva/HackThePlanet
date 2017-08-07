using System.Collections.Generic;
using HackThePlanetApp.Data.Models;

namespace HackThePlanetApp.Services.Contracts
{
    public interface IEquipmentService
    {
         IEnumerable<Equipment> GetAllEquipment();

         Equipment GetEquipmentById(int id);         
    }
}