using HotelApiv3.Authentication;
using HotelApiv3.Data;
using HotelApiv3.Data.Models;
using HotelApiv3.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HotelApiv3.Controllers
{
    
    public class HomeController : ControllerBase
    {

        public EntityService _entityService;
        public HashService _hashService;
        
        public HomeController(EntityService entityService, HashService hashService)
        {
            _entityService = entityService;
            _hashService = hashService;
            
        }

       
        [HttpGet("Search Customers")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public IActionResult SearchEntity(string name, string surname)
        {
            bool checkDbConnection = _entityService.CheckDatabaseConnection();
            
            if(checkDbConnection) 
            {
                var entities = _entityService.SearchEntities(name, surname);

                if(entities.Count > 0)
                {
                    return Ok(entities);
                }
                else
                {
                    var errorResponse = new { error = new { code = 404, message = "Customer not found" } };
                    var json = JsonSerializer.Serialize(errorResponse);
                    return NotFound(json);
                }

            }
            else
            {
                var errorResponse = new { error = new { code = 404, message = "Database not connected" } };
                var json = JsonSerializer.Serialize(errorResponse);
                return NotFound(json);
            }
            
        }


        [HttpGet("Set Properties")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public IActionResult SetProperties(string connectionstring, string querystring, string name, string surname)
        {

            using (var sw = new StreamWriter("TestFile.txt"))
            {
                List<string> strContent = new List<string>()
                 {
                    connectionstring,
                    querystring,
                    name,
                    surname
                 };

                for (int i = 0; i < strContent.Count; i++)
                {
                    sw.WriteLine(strContent[i]);
                }
            }
           

        var successResponse = new { success = true, message = "Properties set successfully" };
        var json = JsonSerializer.Serialize(successResponse);
        return Ok(json);
        }


    }
}
