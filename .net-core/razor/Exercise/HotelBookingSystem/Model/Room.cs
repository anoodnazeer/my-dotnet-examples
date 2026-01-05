using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Model
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomType { get; set; } // Single, Double, Deluxe

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        //public ICollection<Booking> Bookings { get; set; }
    }
}
