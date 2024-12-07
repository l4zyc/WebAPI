using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class Student
{
    [Key]
    [RegularExpression("ST%")]
    [MaxLength(5)]
    public String StudentID { get; set; }
    [MaxLength(50)]
    public String FirstName {get; set;}
    [MaxLength(50)]
    public String LastName {get; set;}
    public int Age { get; set; }
    [ForeignKey("Major")]
    public int MajorID {get; set;}
    public Major Major {get; set;}
}
