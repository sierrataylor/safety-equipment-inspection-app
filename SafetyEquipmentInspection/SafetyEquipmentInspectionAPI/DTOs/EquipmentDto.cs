using Google.Cloud.Firestore;

namespace SafetyEquipmentInspectionAPI.DTOs
{
    [FirestoreData]
    public class EquipmentDto
    {
        [FirestoreProperty(ConverterType = typeof(GuidConverter))]
        public Guid EquipmentId { get; set; }
        [FirestoreProperty]
        public string EquipmentType { get; set; }
        [FirestoreProperty]
        public string Location { get; set; }
        [FirestoreProperty]
        public string Building { get; set; }
        [FirestoreProperty]
        public int Floor { get; set; }
    }
    public class GuidConverter : IFirestoreConverter<Guid>
    {
        public object ToFirestore(Guid value) => value.ToString("N");

        public Guid FromFirestore(object value)
        {
            switch (value)
            {
                case string guid: return Guid.ParseExact(guid, "D");
                case null: throw new ArgumentNullException(nameof(value));
                default: throw new ArgumentException($"Unexpected data: {value.GetType()}");
            }
        }
    }
}
