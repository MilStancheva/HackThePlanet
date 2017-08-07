using System.Collections.Generic;
using HackThePlanetApp.Data.Models;

namespace HackThePlanetApp.ViewModels.EquipmentViewModels
{
    public class EquipmentListViewModel
    {
        public IEnumerable<Equipment> Equipment { get; set; }      
    }
}