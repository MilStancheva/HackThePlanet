using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HackThePlanetApp.Data.Models;
using HackThePlanetApp.Data.Repositories.Contracts;

namespace HackThePlanetApp.Data.Repositories
{
    public class EquipmentTypeRepository : IEquipmentTypeRepository, IDapperRepository<EquipmentType>
    {
        private const string SqlSelectAllEquipmentTypeQuery = @"SELECT * FROM equipmenttype";
        private const string SqlSelectAllEquipmentQuery = @"SELECT * FROM equipment";      

        private readonly IAppDb dbContext;

        public EquipmentTypeRepository(IAppDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<EquipmentType> GetAll()
        {
            var equipmentTypesList = new List<EquipmentType>();
            var equipmentsList = new List<Equipment>();

            using (IDbConnection dbConnection = dbContext.DbConnection)
            {
                dbConnection.Open();
                equipmentTypesList = dbConnection.Query<EquipmentType>(SqlSelectAllEquipmentTypeQuery).AsList();
                equipmentsList = dbConnection.Query<Equipment>(SqlSelectAllEquipmentQuery).AsList();

                foreach (var equipmentType in equipmentTypesList)
                {
                    foreach (var equipment in equipmentsList)
                    {
                        if (equipment.EquipmentType == equipmentType.Id)
                        {
                            if (!equipmentType.Equipments.Contains(equipment))
                            {
                                equipmentType.Equipments.Add(equipment);
                            }
                        }

                    
                    }
                }

                return equipmentTypesList;
            }
        }
    }
}