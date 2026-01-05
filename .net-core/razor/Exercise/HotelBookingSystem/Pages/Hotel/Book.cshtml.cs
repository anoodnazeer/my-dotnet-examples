using HotelBookingSystem.Data;
using HotelBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace HotelBookingSystem.Pages.Hotel
{
    public class BookModel : PageModel
    {
        private readonly HotelDbContext _context;

        public BookModel(HotelDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var room = await _context.Rooms.FindAsync(Booking.RoomId);
            if (room == null || !room.IsAvailable)
            {
                ModelState.AddModelError(string.Empty, "Room is not available.");
                return Page();
            }

            Booking.Status = "Booked";
            _context.Bookings.Add(Booking);
            room.IsAvailable = false;
            await _context.SaveChangesAsync();

            return RedirectToPage("Confirmation", new { id = Booking.Id });
        }
    }
}

