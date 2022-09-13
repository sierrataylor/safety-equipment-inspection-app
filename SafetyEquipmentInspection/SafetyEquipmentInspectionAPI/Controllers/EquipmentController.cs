using Microsoft.AspNetCore.Mvc;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    public class EquipmentController : FirestoreController, IEquipmentController
    {
        public EquipmentController()
        {
        }

        [HttpGet("GetAllEquipment")]
        public void GetAllEquipment()
        {

        }
    }
}
