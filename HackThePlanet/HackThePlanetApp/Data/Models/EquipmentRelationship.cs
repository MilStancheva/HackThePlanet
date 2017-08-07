namespace HackThePlanetApp.Data.Models
{
    public class EquipmentRelationship
    {
        public int Id { get; set; }

        public int FromEquipment { get; set; }

        public int ToEquipment { get; set; }
    }
}