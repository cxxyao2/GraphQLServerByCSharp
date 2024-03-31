using GraphQLDemo2.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo2.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; } 
        public DbSet<Instructor> Instructors { get; set; } 

        public DbSet<Student> Students { get; set; }    

      
    }
}
