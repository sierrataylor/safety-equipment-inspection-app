using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    public class InspectionController
    {
        public readonly FirestoreDb _db;
        public InspectionController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);

        }

        [HttpPost("/inspection/")]
        public async Task StartInspection(string equipmentId, string inspectionDate, string result, string reviewer)
        {
            InspectionDto inspectionDto = new InspectionDto
            {
                InspectionId = Guid.NewGuid().ToString(),
                EquipmentId = equipmentId,
                InspectionResult = result,
                ReviewerId = reviewer,
                LastInspectionDate = DateTime.Parse(inspectionDate)
            };
            string message;
            var inspectionCollection = _db.Collection("Inspection");
            var equipmentCollection = _db.Collection("Equipment");
            var equipmentBeingInspected = await equipmentCollection.Document(equipmentId).GetSnapshotAsync();
            var inspectionDocument = await inspectionCollection.Document(inspectionDto.InspectionId).GetSnapshotAsync();

            //check if inspection hasn't been performed before and the item being inspected exist in db before continuing
            if (!inspectionDocument.Exists & equipmentBeingInspected.Exists)
            {
                Dictionary<string, object> inspectionDictionary = inspectionDocument.ToDictionary();
                await inspectionCollection.Document(inspectionDto.InspectionId).SetAsync(inspectionDictionary);
                message = JsonConvert.SerializeObject(inspectionDictionary);
            }
            else
            {
                message = $"Cannot complete this inspection";
            }
        }
    }
}
