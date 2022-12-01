using System;
using System.Net.Http;
using System.Web;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    [ApiController]
    public class LoginController
    {
        public readonly FirestoreDb _db;
        public LoginController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }

        [Route("/")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeDoc = await employeesCollection.WhereEqualTo("EmployeeId", username).GetSnapshotAsync();
                if (employeeDoc.First().Exists)
                {
                    EmployeeDto employeeObj = employeeDoc.First().ConvertTo<EmployeeDto>();
                    if (employeeObj.Password.Equals(password))
                    {
                        return new OkResult();
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}.");
            }
        }
    }
}
