using MongoDB.Bson;

namespace EAD.Models
{
    public class FuelDetails
    {
        public ObjectId Id { get; set; }
        public string FuelType { get; set; }
        public string Capacity { get; set; }
        public string IsArrival { get; set; }
        public string FuelStationId { get; set; }
    }
}
