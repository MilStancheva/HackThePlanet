using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using HackThePlanetApp.Data.Models;
using HackThePlanetApp.Data.Repositories.Contracts;

namespace HackThePlanetApp.Data.Repositories
{
    public class EquipmentRepository : IEquipmentRepository, IDapperRepository<Equipment>
    {
        private const int EquipmentTypeId = 1;
        private const string SqlSelectAllEquipmentTypeQuery = "SELECT * FROM equipmenttype;";
        private const string SqlSelectAllEquipmentQuery = "SELECT * FROM equipment;";
        private const string SqlSelectAllRelationshipsQuery = "SELECT * FROM relationships;";
        private const string SqlSelectAllEquipmentOfTypeQuery = "SELECT * FROM equipment as e WHERE e.equipmentType = 1;";
        private string multiQueryGetAll = string.Join("\n", new List<string>() { SqlSelectAllEquipmentTypeQuery, SqlSelectAllEquipmentQuery, SqlSelectAllRelationshipsQuery, SqlSelectAllEquipmentOfTypeQuery });
        private const string SqlSelectEquipmentByIdQuery = "SELECT * FROM equipment as e WHERE e.id = @id;";
        private const string SqlSelectRelationshipsByIdQuery = "SELECT * FROM relationships as r WHERE r.fromEquipment = @id;";
        private const string SqlSelectEquipmentListByIdQuery = @"SELECT * FROM equipment as e
Where e.id in(@id1, @id2, @id3);";
        private string multiQueryGetById = string.Join("\n", new List<string>() { SqlSelectAllEquipmentTypeQuery, SqlSelectEquipmentByIdQuery, SqlSelectRelationshipsByIdQuery });

        private readonly IAppDb dbContext;

        public EquipmentRepository(IAppDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Equipment> GetAll()
        {
            var equipmentTypesList = new List<EquipmentType>();
            var equipmentsList = new List<Equipment>();
            var relationshipsList = new List<EquipmentRelationship>();
            var allComputers = new List<Equipment>();
            var result = new List<Equipment>();

            using (IDbConnection dbConnection = dbContext.DbConnection)
            {
                dbConnection.Open();
                using (var multi = dbConnection.QueryMultiple(this.multiQueryGetAll, null))
                {
                    equipmentTypesList = multi.Read<EquipmentType>().AsList();
                    equipmentsList = multi.Read<Equipment>().AsList();
                    relationshipsList = multi.Read<EquipmentRelationship>().AsList();
                    allComputers = multi.Read<Equipment>().AsList();

                    this.SetEquipmentTypeNamesToAllEquipment(equipmentsList, equipmentTypesList);

                    foreach (var computer in allComputers)
                    {
                        int id = computer.Id;
                        Equipment currentComputer = equipmentsList.FirstOrDefault(c => c.Id == id);
                        result.Add(currentComputer);
                        foreach (var relation in relationshipsList)
                        {
                            if (relation.FromEquipment == currentComputer.Id)
                            {
                                this.SetEquipmentCollectionToEquipmentFromRelationship(equipmentsList, relation, currentComputer);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public Equipment GetById(int id)
        {
            var equipmentTypesList = new List<EquipmentType>();
            var relationshipsList = new List<EquipmentRelationship>();
            var equipmentsList = new List<Equipment>();
            Equipment currentComputer = null;

            using (IDbConnection dbConnection = dbContext.DbConnection)
            {
                dbConnection.Open();
                using (var multi = dbConnection.QueryMultiple(this.multiQueryGetById, new { id = id }))
                {
                    equipmentTypesList = multi.Read<EquipmentType>().AsList();
                    currentComputer = multi.Read<Equipment>().Single();
                    relationshipsList = multi.Read<EquipmentRelationship>().AsList();

                    var equipmentIds = relationshipsList.Select(x => x.ToEquipment).ToArray();
                    equipmentsList = dbConnection.Query<Equipment>(SqlSelectEquipmentListByIdQuery, new { id1 = equipmentIds[0], id2 = equipmentIds[1], id3 = equipmentIds[2] }).AsList();

                    currentComputer.TypeName = equipmentTypesList
                                                .FirstOrDefault(x => x.Id == currentComputer.EquipmentType).Name;
                    this.SetEquipmentTypeNamesToAllEquipment(equipmentsList, equipmentTypesList);

                    foreach (var relation in relationshipsList)
                    {
                        this.SetEquipmentCollectionToEquipmentFromRelationship(equipmentsList, relation, currentComputer);
                    }
                }
            }

            return currentComputer;
        }

        private void SetEquipmentCollectionToEquipmentFromRelationship(IEnumerable<Equipment> equipmentsList, EquipmentRelationship relation, Equipment currentComputer)
        {
            var equipment = equipmentsList.FirstOrDefault(x => x.Id == relation.ToEquipment);
            if (equipment == null)
            {
                throw new ArgumentException("Equipment is not found");
            }

            if (!currentComputer.Equipments.Contains(equipment))
            {
                currentComputer.Equipments.Add(equipment);
            }
        }

        private void SetEquipmentTypeNamesToAllEquipment(IEnumerable<Equipment> equipmentsList, IEnumerable<EquipmentType> equipmentTypesList)
        {
            foreach (var equipment in equipmentsList)
            {
                foreach (var equipmentType in equipmentTypesList)
                {
                    if (equipment.EquipmentType != equipmentType.Id)
                    {
                        continue;
                    }

                    string name = equipmentType.Name;
                    equipment.TypeName = name;
                }
            }
        }
    }
}