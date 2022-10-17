using EAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            return new JsonResult(datalist);
        }
        [HttpPost]
        public JsonResult Post(FuelStation fuelStation)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            int lastFuelStationId = client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").AsQueryable().Count();
            fuelStation.FuelStationId = lastFuelStationId + 1;
            client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").InsertOne(fuelStation);
            return new JsonResult("Adding Successfull");
        }
        [HttpPut]
        public JsonResult Put(FuelStation fuelStation)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelStation>.Filter.Eq("FuelStationId",fuelStation.FuelStationId);
            var update = Builders<FuelStation>.Update.Set("FuelStationName", fuelStation.FuelStationName).Set("Location", fuelStation.Location);
            client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").UpdateOne(filter,update);
            return new JsonResult("Update Successfull");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelStation>.Filter.Eq("FuelStationId",id);
            client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").DeleteOne(filter);
            return new JsonResult("Delete Successfull");
        }
        [HttpGet("{location}")]
        public JsonResult SearchByName(string location)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelStation>("FuelStation").Find(location);
            return new JsonResult(datalist);
        }

    }
}
