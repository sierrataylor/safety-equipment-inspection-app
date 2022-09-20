using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    public class EquipmentController : IEquipmentController
    {
        public readonly FirestoreDb _db;
        public EquipmentController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }

        [HttpPost("AddEquipmentPiece")]
        public async Task AddEquipmentPiece(EquipmentDto equipmentDto)
        {
            var equipmentCollection = _db.Collection("Equipment");
            var json = JsonConvert.SerializeObject(equipmentDto);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            await equipmentCollection.AddAsync(dictionary);
        }
    }
}
