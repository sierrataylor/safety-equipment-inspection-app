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

        [HttpGet("/employees/employee/{employeeId}")]
        public async Task<string> GetEmployee(string employeeId)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeDoc = await employeesCollection.Document(employeeId).GetSnapshotAsync();

                var employee = employeeDoc.ConvertTo<EmployeeDto>();
                var employeeJson = JsonConvert.SerializeObject(employee);
                return employeeDoc.Exists ?
                    JsonConvert.SerializeObject(new { employee = employeeJson }) :
                    $"Employee {employeeId} not found";
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject( new { error = ex.Message});
            }
            
        }
        [HttpPost("/employees/addEmployee")]
        public async Task<string> AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeDoc = await employeesCollection.Document(employeeDto.EmployeeId).GetSnapshotAsync();
                string message;
                
                if (!employeeDoc.Exists)
                {
                    var empJson = JsonConvert.SerializeObject(employeeDto);
                    Dictionary<string, object> employeeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(empJson);
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

        [HttpGet("/employees/")]
        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            try
            {
                List<EmployeeDto> employees = new List<EmployeeDto>();
                var employeesCollection = _db.Collection("Employee");
                var allEmployeesDocs = await employeesCollection.GetSnapshotAsync();
                foreach (var employeeDoc in allEmployeesDocs)
                {
                    var employee = employeeDoc.ConvertTo<EmployeeDto>();
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("employees/edit/{employeeId}")]
        public async Task<string> UpdateEmployee(string employeeId, string firstName, string lastName, string role, string email)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeToBeUpdated = await employeesCollection.Document(employeeId).GetSnapshotAsync();
                if (employeeToBeUpdated.Exists)
                {
                    EmployeeDto employeeDto = new EmployeeDto
                    {
                        EmployeeId = employeeId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Role = role
                    };
                    var updateJson = JsonConvert.SerializeObject(employeeDto);
                    Dictionary<string, object> updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(updateJson);
                    await employeesCollection.Document(employeeDto.EmployeeId).UpdateAsync(updatesDictionary);
                    return JsonConvert.SerializeObject(new { message = $"Update of {employeeDto.EmployeeId} successfully" });
                }else
                {
                    return $"Employee {employeeId} not found";
                }

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [HttpDelete("employees/delete/{employeeId}")]
        public async Task<string> DeleteEmployee(string employeeId)
        {
            try
            {
                var employeesCollection = _db.Collection("Employee");
                var employeeDocToBeDeleted = await employeesCollection.Document(employeeId).GetSnapshotAsync();
                if (employeeDocToBeDeleted.Exists)
                {
                    Dictionary<string, object> result = employeeDocToBeDeleted.ToDictionary();
                    var empResultJson = JsonConvert.SerializeObject(result);
                    var employeeDataTransferObj = JsonConvert.DeserializeObject<EmployeeDto>(empResultJson);
                    await employeesCollection.Document(employeeId).DeleteAsync();
                    return $"Employee {employeeDataTransferObj.FirstName} {employeeDataTransferObj.LastName} with " +
                            $"ID {employeeDataTransferObj.EmployeeId} deleted";
                }
                else
                {
                    return $"Employee {employeeId} does not exist";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}
