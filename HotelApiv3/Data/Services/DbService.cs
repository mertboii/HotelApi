using Microsoft.EntityFrameworkCore;

namespace HotelApiv3.Data.Services
{
    public class DbService
    {

        public HashService _hashService;
       
        public DbService(HashService hashService)
        {
            _hashService = hashService;
            
        }

        public string GetConnectionString()
        {
            string filePath = "TestFile.txt";
            string connectionString = "defaultconnectionstring";


            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0 && !string.IsNullOrWhiteSpace(lines[0]))
                {
                    connectionString = lines[0];
                    connectionString = _hashService.decrypt(connectionString);  
                    return connectionString;
                }
                else
                {
                    //file is empty
                    return connectionString;
                }
            }
            else
            {
                //file does not exist
                return connectionString;
            }

        }

    }
}
