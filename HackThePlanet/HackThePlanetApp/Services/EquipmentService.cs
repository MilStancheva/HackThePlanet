using System;
using System.Collections.Generic;
using HackThePlanetApp.Data.Models;
using HackThePlanetApp.Data.Repositories;
using HackThePlanetApp.Data.Repositories.Contracts;
using HackThePlanetApp.Services.Contracts;

namespace HackThePlanetApp.Services
{
    public class EquipmentService : IEquipmentService
    {        
        private readonly IEquipmentRepository equipmentRepository;
        private string exceptionMessageText = "Equipment repository canot be null";

        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            if (equipmentRepository == null)
            {
                throw new ArgumentException(exceptionMessageText);
            }

            this.equipmentRepository = equipmentRepository;
        }

        public IEnumerable<Equipment> GetAllEquipment()
        {
            var allComputers = this.equipmentRepository.GetAll();

            return allComputers;
        }

        public Equipment GetEquipmentById(int id)
        {
            Equipment equipment = this.equipmentRepository.GetById(id);

            return equipment;
        }
    }
}