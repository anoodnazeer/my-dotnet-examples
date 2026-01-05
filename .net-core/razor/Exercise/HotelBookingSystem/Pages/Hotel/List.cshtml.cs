using HotelBookingSystem.Data;
using HotelBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Pages.Hotel
{
    public class ListModel : PageModel
    {
        private readonly HotelDbContext _context;
        public IList<Room> Rooms { get; set; }

        public ListModel(HotelDbContext context) => _context = context;

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.AsNoTracking().ToListAsync();
        }
    }
}
