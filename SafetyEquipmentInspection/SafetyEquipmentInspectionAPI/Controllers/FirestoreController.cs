using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    public class FirestoreController : ControllerBase, IFirestoreController
    {
        public readonly FirestoreDb _db;
        public FirestoreController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }
    }
}
