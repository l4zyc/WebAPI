using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Request;

public class UpdateStudentRequest
{
    [Required]
    [StringLength(50)]
    public String FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public String ?LastName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public int MajorID {get; set;}
}
