using System;
using System.Collections.Generic;
using HackThePlanetApp.Data.Models;
using HackThePlanetApp.Data.Repositories;
using HackThePlanetApp.Data.Repositories.Contracts;
using HackThePlanetApp.Services.Contracts;

namespace HackThePlanetApp.Services
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IEquipmentTypeRepository equipmentTypeRepository;
        private string exceptionMessageText = "EquipmentType repository canot be null";

        public EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository)
        {
            if(equipmentTypeRepository == null)
            {
                throw new ArgumentException(exceptionMessageText);
            }

            this.equipmentTypeRepository = equipmentTypeRepository;
        }
        public IEnumerable<EquipmentType> GetAll()
        {
           var allEquipmentTypes = this.equipmentTypeRepository.GetAll();

           return allEquipmentTypes;
        }
    }
}