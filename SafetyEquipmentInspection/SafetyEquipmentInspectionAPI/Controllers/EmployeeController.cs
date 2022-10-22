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

        [HttpGet("/employees/employee/{employeeId}")]
        public async Task<string> GetEmployee(string employeeId)
        {
            try
            {
                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeDoc = await employeesCollection.Document(employeeId).GetSnapshotAsync();

                EmployeeDto employee = employeeDoc.ConvertTo<EmployeeDto>();

                JsonSerializerSettings settings = new JsonSerializerSettings { 
                    Formatting = Formatting.Indented, 
                    ContractResolver = new DefaultContractResolver { 
                        NamingStrategy = new CamelCaseNamingStrategy() 
                    } 
                };

                return employeeDoc.Exists ?
                    JsonConvert.SerializeObject(employee, settings) :


                    $"Employee {employeeId} not found";
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { error = ex.Message });
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
                    JsonSerializerSettings settings = new JsonSerializerSettings { 
                        Formatting = Formatting.Indented, 
                        ContractResolver = new DefaultContractResolver { 
                            NamingStrategy = new CamelCaseNamingStrategy() 
                        } 
                    };
                    message = JsonConvert.SerializeObject(employeeDict, settings);
                }
                else
                {
                    message = $"Employee {employeeId} already exists";
                }
                return message;

            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { error = ex.Message });
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
                CollectionReference employeesCollection = _db.Collection("Employee");
                DocumentSnapshot employeeToBeUpdated = await employeesCollection.Document(employeeId).GetSnapshotAsync();
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
                    string updateJson = JsonConvert.SerializeObject(employeeDto);
                    Dictionary<string, object> updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(updateJson);
                    await employeesCollection.Document(employeeDto.EmployeeId).UpdateAsync(updatesDictionary);
                    return JsonConvert.SerializeObject(new { message = $"Update of {employeeDto.EmployeeId} successfully" });
                }
                else
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
