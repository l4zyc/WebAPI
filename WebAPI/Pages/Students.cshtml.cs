using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Pages.Student
{
    public class StudentModel : PageModel 
    {
        private readonly AppDBContext _context;
        private readonly ILogger _logger;

        public StudentModel(AppDBContext context, ILogger<StudentModel> logger) {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

         //to bind the properties (the getters and setters) of the PageModel to incoming request data
        [BindProperty]
        public WebAPI.Models.Request.StudentRequest? request {get; set;}
        [TempData]
        public String StatusMessage { get; set; } = String.Empty;

        public async Task<IActionResult> OnPostAsync() {
            // if the form data sent by the user is valid
            ModelState.Remove("request.StudentID"); //Remove StudentID from Required Field

            String studentID;
            var studentData = await _context.Student.OrderByDescending(x => x.StudentID).FirstOrDefaultAsync();

            if(studentData == null) {
                studentID = "ST001";
                return Page();
            }

            String latStudentID = studentData.StudentID.Substring(2);
            Int32 incr = Int32.Parse(latStudentID) + 1;
            studentID = $"ST{incr:D3}";

            var student = new Models.Student {
                StudentID = studentID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                MajorID = request.MajorID
            };


            if(!ModelState.IsValid) {
                StatusMessage = "Error";
                _logger.LogWarning("ModelState is invalid.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"ModelState error: {error.ErrorMessage}");
                }
                return Page();
            }

            if(student != null) _context.Student.Add(student);
            await _context.SaveChangesAsync();

            StatusMessage = "Success";

            request = null;
            ModelState.Clear();

            return Page();
        }
    }
}
