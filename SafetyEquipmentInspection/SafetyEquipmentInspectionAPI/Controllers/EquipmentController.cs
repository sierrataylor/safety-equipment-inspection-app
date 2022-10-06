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
                var equipmentDocument = await equipmentCollection.Document(id).GetSnapshotAsync();
                if (equipmentDocument.Exists)
                {
                    //if document exists, use FireStore ConvertTo function to convert it to a DTO
                    var equipmentItem = equipmentDocument.ConvertTo<EquipmentDto>();
                    var resultJson = JsonConvert.SerializeObject(equipmentItem);
                    //return JSON of the added item
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

        [HttpGet("equipment/items/{equipmentType}")]
        public async Task<List<EquipmentDto>> GetListItems(string equipmentType)
        {
            try
            {            
                List<EquipmentDto> equipmentItems = new List<EquipmentDto>();
                var equipmentCollection = _db.Collection("Equipment");
                var getAllItemsQuery = await equipmentCollection.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
                if (getAllItemsQuery.Any())
                {
                    foreach (var item in getAllItemsQuery.Documents)
                    {
                        var itemDataTransferObj = item.ConvertTo<EquipmentDto>();
                        equipmentItems.Add(itemDataTransferObj);
                    }
                }

                return equipmentItems;
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost("equipment/addItem")]
        public async Task<string> AddEquipmentPiece(string equipmentType, string building, int floor, string location)
        {
            try
            {
                EquipmentDto equipmentDto = new EquipmentDto
                {
                    EquipmentId = Guid.NewGuid(),
                    EquipmentType = equipmentType.ToLower(),
                    Building = building.ToUpper(),
                    Floor = floor,
                    Location = location.ToUpper()
                };
                string message;
                var equipmentCollection = _db.Collection("Equipment");
                var dtoJson = JsonConvert.SerializeObject(equipmentDto);
                var itemDocDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dtoJson);
                //check if document already exists with the equipment ID
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

        [HttpPut("equipment/updateItem/{equipmentId}")]
        public async Task<string> UpdateItem(string equipmentId, string equipmentType, string location, int floor, string building)
        {
            try
            {
                var equipmentCollection = _db.Collection("Equipment");
                var itemDocToBeUpdated = await equipmentCollection.Document(equipmentId).GetSnapshotAsync();


                if (itemDocToBeUpdated.Exists)
                {
                    EquipmentDto equipmentDto = new EquipmentDto
                    {
                        EquipmentId = Guid.Parse(equipmentId),
                        EquipmentType = equipmentType,
                        Location = location,
                        Building = building,
                        Floor = floor
                    };
                    //get async snapshot of this document; null if query.document.Count = 0 (meaning the equipmentId was not found)
                    var dtoJson = JsonConvert.SerializeObject(equipmentDto);
                    var updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dtoJson);
                    await equipmentCollection.Document(equipmentId).UpdateAsync(updatesDictionary);
                };

                return JsonConvert.SerializeObject(new { message = $"Update of item {equipmentId} successful" });
            }
            catch (Exception)
            {

                throw;
            }

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
