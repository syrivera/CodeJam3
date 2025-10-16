using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main()
    {

        using (var db = new SchoolDbContext())
        {
            if (!db.Database.CanConnect())
            {
                Console.WriteLine("Could not connect to database.");
                return;
            }

            // TODO: Write your first EF query

            //var students = db.Students.ToList();

            //foreach (var student in students)
            //{
            //    Console.WriteLine($"{student.FirstName} {student.LastName}");
            //}

        }

        // You'll un-comment this later
        HandleCommandInput();
    }

    static void HandleCommandInput()
    {
        var exit = false;
        do
        {
            Console.Write("SIS database> ");
            var input = (Console.ReadLine() ?? "").Split(" ");
            switch (input)
            {
                case ["quit"]:
                    exit = true;
                    Console.WriteLine("Quitting...");
                    break;
                case ["student", var github]:
                    GetStudentByGitHub(github);
                    break;
                case ["new_student", var firstName, var lastName, var github]:
                    AddStudent(firstName, lastName, github);
                    break;
                case ["assignment", .. var name]:
                    GetAssignmentByName(string.Join(" ", name));
                    break;
                case ["assign_grade", var github, .. var name, var rawScore]:
                    try
                    {
                        var score = int.Parse(rawScore);
                        AssignGrade(github, string.Join(" ", name), score);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid score.");
                    }
                    break;
                case ["grade", var github, .. var name]:
                    GetGradeByGitHubAndAssignment(github, string.Join(" ", name));
                    break;
                case [_]:
                    Console.WriteLine("Invalid command (run command 'quit' to exit).");
                    break;
            }
        } while (!exit);
    }

    static void GetStudentByGitHub(string github)
    {
        using (var db = new SchoolDbContext())
        {
            var student = db.Students.FirstOrDefault(s => s.GitHub == github);
            if (student != null)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
                Console.WriteLine($"Student ID: {student.StudentId}");
                Console.WriteLine($"GitHub: {student.GitHub}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }

    static void AddStudent(string firstName, string lastName, string github)
    {
        using (var db = new SchoolDbContext())
        {
            var newStudent = new Student
            {
                StudentId = 0,
                FirstName = firstName,
                LastName = lastName,
                GitHub = github
            };
            db.Add(newStudent);
            db.SaveChanges();

            Console.WriteLine($"Successfully added student: {firstName} {lastName}");
        }
    }

    static void GetAssignmentByName(string assignmentName)
    {
        using (var db = new SchoolDbContext())
        {
            var assignment = db.Assignments.FirstOrDefault(s => s.Name == assignmentName);
            if (assignment != null)
            {
                Console.WriteLine($"{assignment.Name} {assignment.MaxScore}");

            }
            else
            {
                Console.WriteLine("Assignment not found.");
            }
        }
    }

    static void AssignGrade(string github, string assignmentName, int score)
    {
        using (var db = new SchoolDbContext())
        {
            // Find the student by GitHub username
            var student = db.Students.FirstOrDefault(s => s.GitHub == github);
            if (student == null)
            {
                Console.WriteLine($"Student with GitHub '{github}' not found.");
                return;
            }

            // Find the assignment by name
            var assignment = db.Assignments.FirstOrDefault(a => a.Name == assignmentName);
            if (assignment == null)
            {
                Console.WriteLine($"Assignment '{assignmentName}' not found.");
                return;
            }

            // Create and add the grade
            var grade = new Grade
            {
                GradeId = 0,
                StudentId = student.StudentId,
                AssignmentId = assignment.AssignmentId,
                Score = score
            };

            db.Grades.Add(grade);
            db.SaveChanges();

            Console.WriteLine($"Assigned grade {score} to {student.FirstName} {student.LastName} for '{assignmentName}'.");
        }
    }


    static void GetGradeByGitHubAndAssignment(string github, string assignmentName)
    {
        using (var db = new SchoolDbContext())
        {
            // Find the student by GitHub username
            var student = db.Students.FirstOrDefault(s => s.GitHub == github);
            if (student == null)
            {
                Console.WriteLine($"Student with GitHub '{github}' not found.");
                return;
            }

            // Find the assignment by name
            var assignment = db.Assignments.FirstOrDefault(a => a.Name == assignmentName);
            if (assignment == null)
            {
                Console.WriteLine($"Assignment '{assignmentName}' not found.");
                return;
            }

            // Find the grade
            var grade = db.Grades
                .FirstOrDefault(g => g.StudentId == student.StudentId && g.AssignmentId == assignment.AssignmentId);

            if (grade == null)
            {
                Console.WriteLine($"No grade found for {student.FirstName} on '{assignmentName}'.");
            }
            else
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} scored {grade.Score} on '{assignmentName}'.");
            }
        }
    }
}
