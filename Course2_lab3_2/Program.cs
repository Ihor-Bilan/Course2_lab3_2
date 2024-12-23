using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

struct Student
{
    public string LastName;
    public string FirstName;
    public string MiddleName;
    public char Gender;
    public DateTime BirthDate;
    public int MathGrade;
    public int PhysicsGrade;
    public int ITGrade;
    public int Scholarship;

    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName}, {Gender}, {BirthDate:dd.MM.yyyy}, Math: {MathGrade}, Physics: {PhysicsGrade}, IT: {ITGrade}, Scholarship: {Scholarship}";
    }
}

class Program
{
    static List<Student> ReadStudentsFromFile(string filePath)
    {
        var students = new List<Student>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var student = new Student
            {
                LastName = parts[0],
                FirstName = parts[1],
                MiddleName = parts[2],
                Gender = parts[3][0],
                BirthDate = DateTime.ParseExact(parts[4], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                MathGrade = parts[5] == "-" ? 2 : int.Parse(parts[5]),
                PhysicsGrade = parts[6] == "-" ? 2 : int.Parse(parts[6]),
                ITGrade = parts[7] == "-" ? 2 : int.Parse(parts[7]),
                Scholarship = int.Parse(parts[8])
            };
            students.Add(student);
        }

        return students;
    }

    static void PrintMaleStudentsOver18(List<Student> students)
    {
        var today = DateTime.Now;
        var thresholdDate = new DateTime(today.Year, 12, 31);

        var maleStudents = students
            .Where(s => (s.Gender == 'M' || s.Gender == 'Ч') && (today.Year - s.BirthDate.Year > 18 || (today.Year - s.BirthDate.Year == 18 && s.BirthDate.AddYears(18) <= thresholdDate)))
            .ToList();

        foreach (var student in maleStudents)
        {
            Console.WriteLine($"{student.LastName} {student.FirstName} {student.MiddleName}");
        }
    }

    static void Main()

    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var students = ReadStudentsFromFile("data.txt");

        Console.WriteLine("Студенти чоловічої статі старші за 18 років:");
        PrintMaleStudentsOver18(students);
    }
}
