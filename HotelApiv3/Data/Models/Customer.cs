using System.ComponentModel.DataAnnotations;

namespace HotelApiv3.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public int TcNo { get; set; }
        public int RoomNumber { get; set; }

        public string? Name { get; set; }

        public string? SurName { get; set; }
    }
}
