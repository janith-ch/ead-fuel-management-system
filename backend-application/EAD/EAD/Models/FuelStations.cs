using MongoDB.Bson;

namespace EAD.Models
{
    public class FuelStations
    {
        public ObjectId Id { get; set; }
        public int FuelStationId { get; set; }
        public string FuelStationName { get; set; } = "";
        public String Location { get; set; } = "";
    }
}
