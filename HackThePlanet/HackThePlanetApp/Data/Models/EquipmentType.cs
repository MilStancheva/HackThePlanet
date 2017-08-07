using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackThePlanetApp.Data.Models
{
    public class EquipmentType
    {
        private ICollection<Equipment> equipments;

        public EquipmentType()
        {
            this.equipments = new HashSet<Equipment>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments
        {
            get
            {
                return this.equipments;
            }
            set
            {
                this.equipments = value;
            }
        }
    }
}