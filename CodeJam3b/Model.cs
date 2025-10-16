using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("students")]
public class Student
{
    [Key]
    public required int StudentId { get; set; } // Primary Key
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    public required string GitHub { get; set; }
}



[Table("assignments")]
public class Assignment
{
    [Key]
    public required int AssignmentId { get; set; } // Primary Key
    public required string Name { get; set; }
    public required int MaxScore { get; set; }

}

[Table("grades")]
public class Grade {
    [Key]
    public required int GradeId { get; set; } // Primary Key
    public int Score { get; set; }  // Nullable

    // Foreign Keys
    [ForeignKey("Student")]
    public required int StudentId { get; set; }

    public Student? Student { get; set; }

    [ForeignKey("Assignment")]
    public required int AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }

}