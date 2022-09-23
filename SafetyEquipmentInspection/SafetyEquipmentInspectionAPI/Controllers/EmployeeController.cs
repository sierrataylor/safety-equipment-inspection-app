using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI
{
    public class EmployeeController
    {
        public readonly FirestoreDb _db;
        public EmployeeController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }

        [HttpGet("/employees/employee/{id}")]
        public async Task<string> GetEmployee(string employeeId)
        {
            try
            {
                var employeesCollection = _db.Collection("Employees");
                var employeeDoc = await employeesCollection.Document(employeeId).GetSnapshotAsync();
                Dictionary<string, object> result = employeeDoc.ToDictionary();
                var empResultJson = JsonConvert.SerializeObject(result);
                var employeeJson = JsonConvert.DeserializeObject<EmployeeDto>(empResultJson);
                return !employeeDoc.Exists ?
                    JsonConvert.SerializeObject(new { employee = employeeJson }) :
                    $"Employee {employeeId} not found";
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject( new { error = ex.Message});
            }
            
        }
        [HttpPost("/employees/{id}")]
        public async Task<string> AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeDoc = await employeesCollection.Document(employeeDto.EmployeeId).GetSnapshotAsync();
                string message;
                
                if (!employeeDoc.Exists)
                {
                    Dictionary<string, object> employeeDict = employeeDoc.ToDictionary();                   
                    await employeesCollection.Document(employeeDto.EmployeeId).SetAsync(employeeDict);
                    message = JsonConvert.SerializeObject(employeeDict);
                }
                else
                {
                    message = $"Employee {employeeDto.EmployeeId} already exists";
                }
                return message;
                
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject( new { error = ex.Message});
            }
        }

    }
}
