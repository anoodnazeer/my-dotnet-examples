using HotelBookingSystem.Data;
using HotelBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelBookingSystem.Pages.Hotel
{
    public class ConfirmationModel : PageModel
    {
        private readonly HotelDbContext _context;

        public ConfirmationModel(HotelDbContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (Booking == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
