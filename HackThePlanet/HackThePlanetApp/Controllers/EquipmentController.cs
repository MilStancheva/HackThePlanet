using System.Linq;
using System.Text;
using HackThePlanetApp.Data;
using HackThePlanetApp.Data.Models;
using HackThePlanetApp.Data.Repositories;
using HackThePlanetApp.Services;
using HackThePlanetApp.Services.Contracts;
using HackThePlanetApp.ViewModels.EquipmentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HackThePlanetApp.Controllers
{
    public class EquipmentController : Controller
    {
        private IEquipmentService equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        public IActionResult Index()
        {
            var equipmentListViewModel = new EquipmentListViewModel();
            var equipmentList = this.equipmentService.GetAllEquipment();
            equipmentListViewModel.Equipment = equipmentList;

            return View(equipmentListViewModel);
        }

        public FileContentResult Export(int id)
        {
            var model = this.equipmentService.GetEquipmentById(id);            
            var data = this.SetModelToString(model);
            var bytes = UnicodeEncoding.Unicode.GetBytes(data);
            string filename = $"{model.Name}.csv";
            string mime = "text/csv";
            
            return File(bytes, mime, filename);
        }

        private string SetModelToString(Equipment model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id Type Name");
            sb.AppendLine(model.Id + " " + model.TypeName + " " + model.Name);
            foreach(var item in model.Equipments)
            {
                sb.AppendLine(item.Id + " " + item.TypeName + " " + item.Name);
            }

            var result = sb.ToString();

            return result;
        }
    }
}