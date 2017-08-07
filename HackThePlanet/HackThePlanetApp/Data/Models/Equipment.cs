using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackThePlanetApp.Data.Models
{
    public class Equipment
    {
        private ICollection<Equipment> equipments;

        public Equipment()
        {
            this.equipments = new HashSet<Equipment>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual int EquipmentType { get; set; }

        public string TypeName { get; set; }

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