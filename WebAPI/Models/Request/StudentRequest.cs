using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.Request;

public class StudentRequest
{
    [Required]
    public String StudentID { get; set; }
    [Required]
    [StringLength(50)]
    public String FirstName { get; set; }
    [StringLength(50)]
    public String ?LastName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public int MajorID {get; set;}
}
