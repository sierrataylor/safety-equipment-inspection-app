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
        public async Task<string> GetItem(string id)
        {
            try
            {
                //get Equipment collection from NoSQL db
                var equipmentCollection = _db.Collection("Equipment");
                string message;
                //query collection for document with an EquipmentId equal to id and get async snapshot of query result
                var document = await equipmentCollection.Document(id).GetSnapshotAsync();
                if (document != null)
                {
                    /*if the document exists convert it to a dictionary
                     * serialize dictionary to JSON
                     * deserialize that JSON to a DTO
                     * and return the json string of the dictionary*/
                    Dictionary<string, object> result = document.ToDictionary();
                    var resultJson = JsonConvert.SerializeObject(result);
                    EquipmentDto equipmentDto = JsonConvert.DeserializeObject<EquipmentDto>(resultJson);
                    message = resultJson;
                }
                else
                {
                    //if query.Documents has a size of 0, then the document was not found
                    message = $"Item with ID {id} not found";
                }
                return message;
            }
            catch (Exception ex)
            {
                //if document 
                return JsonConvert.SerializeObject(new { error = ex.Message });

            }
        }

        [HttpPost("AddEquipmentPiece")]
        public async Task<string> AddEquipmentPiece(string equipmentType, string buliding, int floor, string location)
        {
            try
            {
                EquipmentDto equipmentDto = new EquipmentDto
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentType = equipmentType,
                    Building = buliding,
                    Floor = floor,
                    Location = location
                };
                string message;
                var equipmentCollection = _db.Collection("Equipment");
                var dtoJson = JsonConvert.SerializeObject(equipmentDto);
                var itemDocDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dtoJson);
                var doc = await equipmentCollection.Document(equipmentDto.EquipmentId.ToString()).GetSnapshotAsync();

                if (!doc.Exists)
                {
                    var docAdded = await equipmentCollection.Document(equipmentDto.EquipmentId.ToString()).SetAsync(itemDocDictionary);
                    message = JsonConvert.SerializeObject(new { message = $"Successfully added item {equipmentDto.EquipmentId}", item = dtoJson }, Formatting.Indented);
                }
                else
                {
                    message = $"Item {equipmentDto.EquipmentId} already in Equipment";
                }
                return message;

            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { error = ex.Message });
            }

        }

        [HttpPut("equipment/updateItem/{id}")]
        public async Task<string> UpdateItem(EquipmentDto equipmentDto)
        {
            var id = equipmentDto.EquipmentId;
            var equipmentCollection = _db.Collection("Equipment");

            var query = await equipmentCollection.WhereEqualTo("EquipmentId", id.ToString()).GetSnapshotAsync();

            //get async snapshot of this document; null if query.document.Count = 0 (meaning the equipmentId was not found)
            var itemDocToBeUpdated = equipmentCollection.Document(query.Documents[0].Id);
            var dtoJson = JsonConvert.SerializeObject(equipmentDto);
            var updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dtoJson);
            await itemDocToBeUpdated.UpdateAsync(updatesDictionary);


            return JsonConvert.SerializeObject(new { message = $"Update of item {id} successful" });


        }

        [HttpDelete("equipment/deleteItem/{id}")]
        public async Task<string> DeleteItem(string id)
        {
            try
            {
                //get Equipment collection from NoSQL db
                var equipmentcollection = _db.Collection("Equipment");

                //query collection for document with an EquipmentId equal to id and get async snapshot of query result
                var query = await equipmentcollection.WhereEqualTo("EquipmentId", id.ToString()).GetSnapshotAsync();
                await equipmentcollection.Document(query.Documents[0].Id).DeleteAsync();
                return $"Deletion of {query.Documents[0].Id} successful";


            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = id, error = ex.Message });
            }
        }
    }
}
