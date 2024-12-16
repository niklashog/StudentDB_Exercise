using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StudentDB_Exercise.Data;

namespace StudentDB_Exercise
{
    public class App
    {
        public void Run()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<StudentContext>();
            options.UseSqlServer(connectionString);


            while (true)
            {
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("1. Create new course");
                Console.WriteLine("2. List all courses");
                Console.WriteLine("3. Create new student");
                Console.WriteLine("4. List all students");
                Console.WriteLine("5. Find student by ID");
                Console.WriteLine("0. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNewCourse(options);
                        break;
                    case "2":
                        ListAllCourses(options);
                        break;
                    case "3":
                        CreateNewStudent(options);
                        break;
                    case "4":
                        ListAllStudents(options);
                        break;
                    case "5":
                        FindStudentById(options);
                        break;
                    case "0":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }

        void CreateNewCourse(DbContextOptionsBuilder<StudentContext> options)
        {
            using (var dbContext = new StudentContext(options.Options))
            {
                var newCourse = new Kur
                {
                    Namn = "Math"
                };

                dbContext.Kurs.Add(newCourse);
                dbContext.SaveChanges();
            }
        }
        void ListAllCourses(DbContextOptionsBuilder<StudentContext> options)
        {
            using (var dbContext = new StudentContext(options.Options))
            {
                var temp = dbContext.Kurs
                    .ToList();

                foreach (var c in temp)
                {
                    Console.WriteLine(c.Namn);
                }
            }
        }
        void CreateNewStudent(DbContextOptionsBuilder<StudentContext> options)
        {
            using (var dbContext = new StudentContext(options.Options))
            {

                Console.WriteLine("Ange Förnamn:  ");
                var studentFirstname = (Console.ReadLine());
                Console.WriteLine("Ange Efternamn:  ");
                var studentSurname = (Console.ReadLine());

                var newStudent = new Student
                {
                    Fornamn = studentFirstname,
                    Efternamn = studentSurname
                };
                dbContext.Students.Add(newStudent);
                dbContext.SaveChanges();
            }
        }
        void ListAllStudents(DbContextOptionsBuilder<StudentContext> options)
        {
            using (var dbContext = new StudentContext(options.Options))
            {
                var myStudents = dbContext.Students.ToList();

                foreach (var student in myStudents)
                {
                    Console.WriteLine($"{student.Id} {student.Fornamn}");
                }
            }
        }
        void FindStudentById(DbContextOptionsBuilder<StudentContext> options)
        {
            using (var dbContext = new StudentContext(options.Options))
            {
                Console.WriteLine("Skriv id: ");
                var studentId = int.Parse(Console.ReadLine());

                var myStudent = dbContext.Students.First(s => s.Id == studentId);

                Console.WriteLine($"{myStudent.Fornamn} {myStudent.Efternamn} ");
            }
        }
        void Exit()
        {
            Environment.Exit(0);
        }
    }
}
