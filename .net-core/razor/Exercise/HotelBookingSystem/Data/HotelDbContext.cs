using HotelBookingSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Data
{
    public class HotelDbContext : DbContext 
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
       : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
