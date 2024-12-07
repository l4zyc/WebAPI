using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Major
{
    [Key]
    public int MajorID { get; set; }
    [MaxLength(50)]
    public String MajorName { get; set; }
    public ICollection<Student> Students { get; set; }
}
