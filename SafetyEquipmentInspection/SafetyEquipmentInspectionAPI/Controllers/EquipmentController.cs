using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Interfaces;
using System.Web;

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

        [HttpGet("/equipment/item/{id}")]
        public async Task<string> GetItem(int id)
        {
            //get Equipment collection from NoSQL db
            var equipmentcollection = _db.Collection("Equipment"); 

            //query collection for document with an EquipmentId equal to id and get async snapshot of query result
            var query = await equipmentcollection.WhereEqualTo("EquipmentId", id.ToString()).GetSnapshotAsync(); 

            try
            {
                if (query.Documents.Count > 0)
                {

                    //get async snapshot of this document; null if query.document.Count = 0 (meaning the equipmentId was not found)
                    var itemDoc = await equipmentcollection.Document(query.Documents[0].Id).GetSnapshotAsync();

                    /*if the document exists convert it to a dictionary
                     * serialize dictionary to JSON
                     * deserialize that JSON to a DTO
                     * and return the json string of the dictionary*/
                    Dictionary<string, object> result = itemDoc.ToDictionary();
                    var resultJson = JsonConvert.SerializeObject(result);
                    EquipmentDto equipmentDto = JsonConvert.DeserializeObject<EquipmentDto>(resultJson);
                    return resultJson;

                }
                else
                {
                    //if document is not found
                    return JsonConvert.SerializeObject(new { id = id, error = $"Item with ID {id} not found"});
                }
            }
            catch (Exception ex)
            {
                //if document 
                return JsonConvert.SerializeObject(new {error = ex.Message});
            }
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
