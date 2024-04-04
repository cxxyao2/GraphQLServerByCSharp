using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.Entities;
using GraphQLDemo2.API.Schema.Filters;
using GraphQLDemo2.API.Schema.Sorters;
using GraphQLDemo2.API.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo2.API.Schema.Queries
{
    public class Query
    {
        [UseDbContext(typeof(DataContext))]
        public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] DataContext context)
        {

            IEnumerable<CourseQueryType> courses = await context.Courses
                .Where(c => c.Name.Contains(term))
                .Select(c => new CourseQueryType()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    InstructorId = c.InstructorId,

                })
                .ToListAsync();

            IEnumerable<InstructorQueryType> instructors = await context.Instructors
               .Where(c => c.FirstName.Contains(term) || c.LastName.Contains(term))
               .Select(c => new InstructorQueryType()
               {
                   Id = c.Id,
                   FirstName = c.FirstName,
                   LastName = c.LastName,
                   Salary = c.Salary
               })
               .ToListAsync();

            return new List<ISearchResultType>()
                .Concat(courses)
                .Concat(instructors);
        
        }

        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "Smash tha like button and subscribe to Singletons";
    }
}
