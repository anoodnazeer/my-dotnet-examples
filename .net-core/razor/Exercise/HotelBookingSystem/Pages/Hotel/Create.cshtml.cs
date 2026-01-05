using HotelBookingSystem.Data;
using HotelBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Reflection.Metadata.BlobBuilder;

namespace HotelBookingSystem.Pages.Hotel
{
    public class CreateModel : PageModel
    {
        private readonly HotelDbContext _context;
        
        [BindProperty] 
        public Room Rooms { get; set; }

        public CreateModel(HotelDbContext context) =>
            _context = context;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _context.Rooms.Add(Rooms);
            await _context.SaveChangesAsync();
            return RedirectToPage("./List");
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)

        //        return Page();
        //    _context.Rooms.Add(Rooms);
        //     _context.SaveChangesAsync();
        //    return RedirectToPage("/MyBook/index");
        //} 
    }
}
