using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo2.API.Services.Courses
{
    public class CoursesRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public CoursesRepository(IDbContextFactory<DataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                return await context.Courses.ToListAsync();
            }
        }

        public async Task<Course> Create(CourseDTO course)
        {

            using (DataContext context=_contextFactory.CreateDbContext())
            {
                Course c = new Course
                {
                   
                    Name = course.Name,
                    Subject = course.Subject,
                    InstructorId = course.InstructorId,
                    Students = new List<Student>(),

                };
                context.Courses.Add(c);
                await context.SaveChangesAsync();

                return c;

            }

        }

        public async Task<Course> Update(CourseDTO course)
        {

            using (DataContext context = _contextFactory.CreateDbContext())
            {
                Course c = new Course
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subject = Subject.MatheMatics,
                    InstructorId = 101,
                    Instructor = new Instructor
                    {
                        Id = 101,
                        FirstName = "John",
                        LastName = "Doe",
                        Salary = 50000
                    },
                    Students = new List<Student>(),

                };
                context.Courses.Add(c);
                await context.SaveChangesAsync();

                return c;

            }

        }

        public async Task<bool> Delete(int id)
        {

            using (DataContext context = _contextFactory.CreateDbContext())
            {
                Course c = new Course
                {
                    Id = id

                };
                context.Courses.Remove(c);
                return await context.SaveChangesAsync() > 0;

                

            }

        }


    }
}
