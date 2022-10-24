using EAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueDetailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public QueueDetailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var datalist = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").AsQueryable();
            return new JsonResult(datalist);
        }
        [HttpPost]
        public JsonResult Post(QueueDetails queueDetails)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            int lastQueueDetailsId = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").AsQueryable().Count();
            queueDetails.QueueId = lastQueueDetailsId + 1;
            client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").InsertOne(queueDetails);
            return new JsonResult("Adding Successfull");
        }
        [HttpPut]
        public JsonResult Put(QueueDetails queueDetails)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<QueueDetails>.Filter.Eq("QueueId", queueDetails.QueueId);
            var update = Builders<QueueDetails>.Update.Set("VehicleType", queueDetails.VehicleType).Set("FuelType", queueDetails.FuelType)
                .Set("Status", queueDetails.Status).Set("FuelStationId", queueDetails.FuelStationId).Set("UserId", queueDetails.UserId);
            client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").UpdateOne(filter, update);
            return new JsonResult("Update Successfull");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<QueueDetails>.Filter.Eq("QueueId", id);
            client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").DeleteOne(filter);
            return new JsonResult("Delete Successfull");
        }
        [HttpGet("{vehicleType}/{fuelType}/{fuelStationId}")]
        public double Queue(string vehicleType, string fuelType, int fuelStationId)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<QueueDetails>.Filter.Eq("VehicleType", vehicleType) & Builders<QueueDetails>.Filter.Eq("FuelType", fuelType)
                 & Builders<QueueDetails>.Filter.Eq("FuelStationId", fuelStationId) & Builders<QueueDetails>.Filter.Eq("Status", "IntheQueue");
            double count = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").Find(filter).Count();
            return count;
        }
        [HttpGet("{UserId}")]
        public JsonResult GetByUserID(string UserId)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<QueueDetails>.Filter.Eq("UserId", UserId);
            var datalist = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").Find(filter).ToList().First();
            return new JsonResult(datalist);
        }
        [HttpGet("{UserId}/{status}")]
        public JsonResult UpdateQueue(int UserId, string status, int fuelStationId)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<QueueDetails>.Filter.Eq("UserId", UserId) & Builders<QueueDetails>.Filter.Eq("Status", "IntheQueue") 
                & Builders<QueueDetails>.Filter.Eq("FuelStationId", fuelStationId);
            var update = Builders<QueueDetails>.Update.Set("Status", status);
            client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").UpdateOne(filter, update);
            return new JsonResult("Update Successfull");
        }
    }
}
