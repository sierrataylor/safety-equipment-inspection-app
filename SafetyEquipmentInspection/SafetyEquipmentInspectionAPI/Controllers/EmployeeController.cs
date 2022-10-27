using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;

namespace SafetyEquipmentInspectionAPI
{
    [ApiController]
    public class EmployeeController
    {
        public readonly FirestoreDb _db;
        public EmployeeController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }
        readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        [HttpGet("/employees/employee/{employeeId}")]
        public async Task<string> GetEmployee(string employeeId)
        {
            try
            {

                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeDoc = await employeesCollection.Document(employeeId).GetSnapshotAsync();


                EmployeeDto employee = employeeDoc.ConvertTo<EmployeeDto>();
                return employeeDoc.Exists ?
                    JsonConvert.SerializeObject(employee, settings) :
                    $"This EmployeeID is not valid, as Employee {employeeId} was not found. Either add this employee to the database or enter a different ID.";
            }
            catch (Exception ex)
            {

                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception."; //JsonConvert.SerializeObject(new { error = ex.Message });
            }

        }
        [HttpPost("/employees/addEmployee")]

        public async Task<string> AddEmployee(string employeeId, string firstName, string lastName, string email, string role, string password)

        {
            try
            {
                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeDoc = await employeesCollection.Document(employeeId).GetSnapshotAsync();
                string message;

                if (!employeeDoc.Exists)
                {

                    EmployeeDto employeeDto = new EmployeeDto
                    {
                        EmployeeId = employeeId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Role = role,
                        Password = password
                    };

                    string empJson = JsonConvert.SerializeObject(employeeDto);
                    Dictionary<string, object> employeeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(empJson);
                    await employeesCollection.Document(employeeDto.EmployeeId).SetAsync(employeeDict);
                    message = JsonConvert.SerializeObject(employeeDict, settings);
                }
                else
                {
                    message = $"Employee {employeeId} already exists in our database. Try viewing the list of employee to ensure that this employee or ID does not already exist, then try adding this employee against with a different ID.";
                }
                return message;

            }
            catch (Exception ex)
            {

                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.";  //JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }

        [HttpGet("/employees/")]
        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            try
            {
                List<EmployeeDto> employees = new List<EmployeeDto>();
                CollectionReference employeesCollection = _db.Collection("Employee");
                QuerySnapshot allEmployeesDocs = await employeesCollection.GetSnapshotAsync();
                foreach (DocumentSnapshot employeeDoc in allEmployeesDocs)
                {
                    EmployeeDto employee = employeeDoc.ConvertTo<EmployeeDto>();
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.");
            }
        }

        [HttpPut("employees/edit/{employeeId}")]
        public async Task<string> UpdateEmployee(string currentEmployeeId, string firstName, string lastName, string role, string email, string updatedEmployeeId = null)
        {
            try
            {
                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeToBeUpdated = await employeesCollection.Document(currentEmployeeId).GetSnapshotAsync();
                if (employeeToBeUpdated.Exists)
                {
                    EmployeeDto employeeDto = new EmployeeDto
                    {
                        EmployeeId = !String.IsNullOrEmpty(updatedEmployeeId) ? updatedEmployeeId : currentEmployeeId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Role = role
                    };
                    string updateJson = JsonConvert.SerializeObject(employeeDto);
                    Dictionary<string, object> updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(updateJson);
                    await employeesCollection.Document(employeeDto.EmployeeId).UpdateAsync(updatesDictionary);
                    return JsonConvert.SerializeObject(new { message = $"Update of {employeeDto.EmployeeId} successfully" });
                }
                else
                {
                    return $"Employee {currentEmployeeId} not found";
                }

            }
            catch (Exception ex)
            {

                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.";
            }
        }

        [HttpDelete("employees/delete/{employeeId}")]
        public async Task<string> DeleteEmployee(string employeeId)
        {
            try
            {
                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeDocToBeDeleted = await employeesCollection.Document(employeeId).GetSnapshotAsync();
                if (employeeDocToBeDeleted.Exists)
                {
                    Dictionary<string, object> result = employeeDocToBeDeleted.ToDictionary();
                    string empResultJson = JsonConvert.SerializeObject(result);
                    await employeesCollection.Document(employeeId).DeleteAsync();
                    EmployeeDto employeeDataTransferObj = JsonConvert.DeserializeObject<EmployeeDto>(empResultJson);
                    return $"Employee {employeeDataTransferObj.FirstName} {employeeDataTransferObj.LastName} with " +
                            $"ID {employeeDataTransferObj.EmployeeId} deleted";
                }
                else
                {
                    return $"Employee {employeeId} does not exist in the database. The EmployeeID you are using may be invalid \n" +
                        $"Please try checking the employee list to ensure that the employee you are trying to delete exists in the database";
                }
            }
            catch (Exception ex)
            {

                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception."; ;
            }
        }
    }
}
