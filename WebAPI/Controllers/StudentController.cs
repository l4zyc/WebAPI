using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.Request;
using WebAPI.Models.Result;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StudentController(AppDBContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<GetStudentResult>> GetResult(String id) {
            try {
                bool isStudentExists = await _context.Student.Where(x => x.StudentID == id).AnyAsync();

                if(!isStudentExists) {
                    throw new ArgumentException("Student Not found");
                }

                var studentData = await _context.Student
                    .Where(x => x.StudentID == id)
                    .Select(x => new GetStudentResult {
                        StudentID = x.StudentID,
                        StudentName = $"{x.FirstName}{x.LastName}",
                        age = x.Age,
                        MajorName = x.Major.MajorName
                    })
                    .FirstOrDefaultAsync();

                var response = new ApiResponse<GetStudentResult> {
                    StatusCode = StatusCodes.Status200OK,
                    HttpMethod = HttpContext.Request.Method,
                    Data = studentData
                };

                return Ok(response);
            } catch(Exception e) {
                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status400BadRequest,
                    HttpMethod = HttpContext.Request.Method,
                    Data = e.Message
                };

                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudentResult>>> Get() {
            try {
                var studentsData = await _context.Student
                    .Include(x => x.Major)
                    .Select(x => 
                        new GetStudentResult {
                            StudentID = x.StudentID,
                            StudentName = $"{x.FirstName} {x.LastName}",
                            age = x.Age,
                            MajorName = x.Major.MajorName
                        }
                    ).ToListAsync();

                var response = new ApiResponse<IEnumerable<GetStudentResult>> {
                    StatusCode = StatusCodes.Status200OK,
                    HttpMethod = HttpContext.Request.Method,
                    Data = studentsData
                };

                return Ok(response);
            } catch(Exception e) {
                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status400BadRequest,
                    HttpMethod = HttpContext.Request.Method,
                    Data = e.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Insert([FromBody] StudentRequest request) {
            try {
                bool isStudentExists = await _context.Student
                    .Where(x => x.StudentID == request.StudentID).AnyAsync();

                bool isMajorExists = await _context.Major
                    .Where(x => x.MajorID == request.MajorID).AnyAsync();

                if(isStudentExists) {
                    throw new ArgumentException("Student Already exists");
                }

                if(!isMajorExists) {
                    throw new ArgumentException("Major does not exist");
                }

                var studentData = new Student {
                    StudentID = request.StudentID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    MajorID = request.MajorID
                };

                _context.Student.Add(studentData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status200OK,
                    HttpMethod = HttpContext.Request.Method,
                    Data = "Data Added"
                };

                return Ok(response);

            } catch(Exception e) {
                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status400BadRequest,
                    HttpMethod = HttpContext.Request.Method,
                    Data = e.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateStudentRequest request, String id) {
            try {
                var studentData = await _context.Student
                    .Where(x => x.StudentID == id)
                    .FirstOrDefaultAsync();
                bool isMajorExists = await _context.Major.Where(x => request.MajorID == x.MajorID).AnyAsync();

                if(studentData == null) {
                    throw new ArgumentException("Student Does not Exist");
                }

                if(!isMajorExists) {
                    throw new ArgumentException("Major does not Exists");
                }

                studentData.FirstName = request.FirstName;
                studentData.LastName = request.LastName;
                studentData.Age = request.Age;
                studentData.MajorID = request.MajorID;

                _context.Student.Update(studentData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status200OK,
                    HttpMethod = HttpContext.Request.Method,
                    Data = $"Student Data {id} Updated"
                };

                return Ok(response);
            } catch(Exception e) {
                var response = new ApiResponse<String> {
                    StatusCode = StatusCodes.Status400BadRequest,
                    HttpMethod = HttpContext.Request.Method,
                    Data = e.Message
                };

                return BadRequest(response);
            }
        }
    
    }
}
