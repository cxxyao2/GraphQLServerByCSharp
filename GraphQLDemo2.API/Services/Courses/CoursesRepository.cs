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

            using (DataContext context = _contextFactory.CreateDbContext())
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

        public async Task<Course> Update(CourseDTO courseDTO)
        {

            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var course = context.Courses.FirstOrDefault(c => c.Id == courseDTO.Id);

                if(course == null)
                {
                    throw new GraphQLException(new Error("Course not found.","COURSE_NOT_FOUND"));
                }
               
                course.Name = courseDTO.Name;
                course.Subject = courseDTO.Subject;
                course.InstructorId = courseDTO.InstructorId;

               
                await context.SaveChangesAsync();

                return course;

            }

        }

        public async Task<bool> Delete(int id)
        {

            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses.FindAsync(id);
                if (course != null)
                {
                    context.Courses.Remove(course);
                }
                return await context.SaveChangesAsync() > 0;

            }

        }


    }
}
