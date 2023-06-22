using HotelApiv3.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HotelApiv3.Data.Services
{
    public class EntityService 
    {
        public AppDbContext _context;
        public HashService _hashService;
       

        public EntityService(AppDbContext context, HashService hashService)
        {
            _context = context;
            _hashService = hashService;

        }


        public List<Customer> SearchEntities(string name,string surname)
        {
            //Querystring
            //SELECT* FROM Customers WHERE Name = {0} AND SurName = { 1 }
                string rawsql = GetRawSql();
                rawsql = _hashService.decrypt(rawsql);
                var customers = _context.Customers
                    .FromSqlRaw(rawsql, name, surname)
                    .ToList();
                return customers;

        }


        public string GetRawSql()
        {
            using (var sr = new StreamReader("TestFile.txt"))
            {
                // Read the stream as a string, and write the string to the console.

                List<string> strContent = new List<string>();

                while (!sr.EndOfStream)
                {
                    strContent.Add(sr.ReadLine());
                }

                return strContent[1];
            }
        }


        public bool CheckDatabaseConnection()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("SELECT 1");


                return true; // Bağlantı başarılı
            }
            catch (Exception)
            {
                return false; // Bağlantı başarısız
            }
        }




    }
}
