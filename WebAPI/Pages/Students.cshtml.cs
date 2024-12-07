using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAPI.Pages.Student
{
    public class StudentModel : PageModel 
    {
        private readonly AppDBContext _context;

        public StudentModel(AppDBContext context) {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

         //to bind the properties (the getters and setters) of the PageModel to incoming request data
        [BindProperty]
        public WebAPI.Models.Student? student {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            // if the form data sent by the user is valid
            if(!ModelState.IsValid) {
                return Page();
            }

            if(student != null) _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./index");
        }
    }
}
