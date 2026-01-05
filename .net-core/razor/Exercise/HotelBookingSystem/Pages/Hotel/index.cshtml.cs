using HotelBookingSystem.Data;
using HotelBookingSystem.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingSystem.Pages.Hotel
{
    public class indexModel : PageModel
    {
        private readonly HotelDbContext _context;

        public indexModel(HotelDbContext context)
        {
            _context = context;
        }

        public IList<Room> AvailableRooms { get; set; }

        public async Task OnGetAsync()
        {
            AvailableRooms = await _context.Rooms
                .AsNoTracking()
                .Where(r => r.IsAvailable)
                .ToListAsync();
        }
    }
}
