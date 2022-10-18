using EAD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var datalist = client.GetDatabase("EADDb").GetCollection<Users>("Users").AsQueryable();
            return new JsonResult(datalist);
        }
        [HttpPost]
        public JsonResult Post(Users users)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            int lastUserId = client.GetDatabase("EADDb").GetCollection<Users>("Users").AsQueryable().Count();
            users.UserId = lastUserId + 1;
            client.GetDatabase("EADDb").GetCollection<Users>("Users").InsertOne(users);
            return new JsonResult("Adding Successfull");
        }
        [HttpPut]
        public JsonResult Put(Users users)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<Users>.Filter.Eq("UserId", users.UserId);
            var update = Builders<Users>.Update.Set("UserName", users.UserName).Set("Password", users.Password)
                .Set("Role", users.Role);
            client.GetDatabase("EADDb").GetCollection<Users>("Users").UpdateOne(filter, update);
            return new JsonResult("Update Successfull");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<Users>.Filter.Eq("UserId", id);
            client.GetDatabase("EADDb").GetCollection<Users>("Users").DeleteOne(filter);
            return new JsonResult("Delete Successfull");
        }
        [HttpGet("{UserName}/{Password}")]
        public JsonResult Queue(string UserName, string Password)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("EADApplicationConnection"));
            var filter = Builders<Users>.Filter.Eq("UserName", UserName) & Builders<Users>.Filter.Eq("Password", Password);
            var datalist = client.GetDatabase("EADDb").GetCollection<Users>("Users").Find(filter).ToList().First();
            return new JsonResult(datalist);
        }
    }
}
