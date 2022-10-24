using System.Text;
using EAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelDetailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FuelDetailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").AsQueryable();
            return new JsonResult(datalist);
        }
        [HttpPost]
        public JsonResult Post(FuelDetails fuelDetails)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            int lastFuelDetailsId = client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").AsQueryable().Count();
            fuelDetails.FuelDetailId = lastFuelDetailsId + 1;
            client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").InsertOne(fuelDetails);
            return new JsonResult("Adding Successfull");
        }
        [HttpPut]
        public JsonResult Put(FuelDetails fuelDetails)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelDetails>.Filter.Eq("FuelDetailId", fuelDetails.FuelDetailId);
            var update = Builders<FuelDetails>.Update.Set("FuelType", fuelDetails.FuelType).Set("Capacity", fuelDetails.Capacity).Set("IsArrival", fuelDetails.IsArrival);
            client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").UpdateOne(filter, update);
            return new JsonResult("Update Successfull");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelDetails>.Filter.Eq("FuelDetailId", id);
            client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").DeleteOne(filter);
            return new JsonResult("Delete Successfull");
        }
        [HttpGet("{fuelType}/{fuelStationId}")]
        public JsonResult AvailableFuelQuotation(string fuelType,int fuelStationId)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<FuelDetails>.Filter.Eq("FuelType", fuelType) & Builders<FuelDetails>.Filter.Eq("IsArrival", true) 
                & Builders<FuelDetails>.Filter.Eq("FuelStationId", fuelStationId); 
            var datalist = client.GetDatabase("EADDb").GetCollection<FuelDetails>("FuelDetails").Find(filter).ToList();

            double total = datalist.Sum(item => item.Capacity);

            var Finished_filter = Builders<QueueDetails>.Filter.Eq("FuelType", fuelType)
                 & Builders<QueueDetails>.Filter.Eq("FuelStationId", fuelStationId) & Builders<QueueDetails>.Filter.Eq("Status", "Finished");
            double Finished_count = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").Find(Finished_filter).Count();

            var IntheQueue_filter = Builders<QueueDetails>.Filter.Eq("FuelType", fuelType)
                & Builders<QueueDetails>.Filter.Eq("FuelStationId", fuelStationId) & Builders<QueueDetails>.Filter.Eq("Status", "IntheQueue");
            double IntheQueue_count = client.GetDatabase("EADDb").GetCollection<QueueDetails>("QueueDetails").Find(IntheQueue_filter).Count();

            var forOneVehicle = 20;

            double finished_fuel = Finished_count * forOneVehicle;
            double remaning_fuel = total - finished_fuel;

            var remaningVehicleCount = remaning_fuel / forOneVehicle;
            remaningVehicleCount = remaningVehicleCount - IntheQueue_count;
            var values = new Dictionary<string, double>();
            values.Add("Capacity", remaning_fuel);
            values.Add("Total_Available", remaningVehicleCount);
            values.Add("In_Queue_count", IntheQueue_count);

            return new JsonResult(values);
        }
    }
}
