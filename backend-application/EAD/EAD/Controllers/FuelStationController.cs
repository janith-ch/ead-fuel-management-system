using EAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FuelStationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").AsQueryable();
            return new JsonResult(datalist);
        }
        [HttpPost]
        public JsonResult Post(FuelStations fuelStation)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            int lastFuelStationId = client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").AsQueryable().Count();
            fuelStation.FuelStationId = lastFuelStationId + 1;
            client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").InsertOne(fuelStation);
            return new JsonResult("Adding Successfull");
        }
        [HttpPut]
        public JsonResult Put(FuelStations fuelStation)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelStations>.Filter.Eq("FuelStationId",fuelStation.FuelStationId);
            var update = Builders<FuelStations>.Update.Set("FuelStationName", fuelStation.FuelStationName).Set("Location", fuelStation.Location)
                .Set("Opentime", fuelStation.Opentime).Set("Closetime", fuelStation.Closetime);
            client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").UpdateOne(filter,update);
            return new JsonResult("Update Successfull");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelStations>.Filter.Eq("FuelStationId",id);
            client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").DeleteOne(filter);
            return new JsonResult("Delete Successfull");
        }
        [HttpGet("{Location}")]
        public JsonResult GetByLocation(string Location)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelStations>.Filter.Eq("Location", Location);
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelStations>("FuelStation").Find(filter).ToList();
            return new JsonResult(datalist);
        }
    }
}
