using MongoDB.Bson;

namespace EAD.Models
{
    public class FuelStation
    {
        public ObjectId Id { get; set; }
        public int FuelStationId { get; set; }
        public string FuelStationName { get; set; }
        public String Location { get; set; }
    }
}
