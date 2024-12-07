using System;

namespace WebAPI.Models.Result;

public class GetStudentResult
{
    public String ?StudentID { get; set; }
    public String ?StudentName { get; set; }
    public int age { get; set; }
    public String ?MajorName { get; set; }
}
