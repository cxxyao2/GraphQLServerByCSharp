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

        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseQueryType>> GetAllCourses()
        {
            var courses = await _coursesRepository.GetAllCourses();

            return courses.Select(c => new CourseQueryType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,


            });

        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        public async Task<IEnumerable<CourseQueryType>> GetCourses()
        {
            var courses = await _coursesRepository.GetAllCourses();

            return courses.Select(c => new CourseQueryType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,


            });

        }


        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        public async Task<IEnumerable<CourseQueryType>> GetOffsetCourses()
        {
            var courses = await _coursesRepository.GetAllCourses();

            return courses.Select(c => new CourseQueryType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,


            });

        }


        [UseDbContext(typeof(DataContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting(typeof(CourseSortType))]
        public IQueryable<CourseQueryType> GetPaginatedCourses([ScopedService] DataContext context)
        {
            return context.Courses.Select(c => new CourseQueryType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,

            });

        }

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
       

        public async Task<Course> GetCourseById(int id)
        {
            await Task.Delay(1000);

            Course course = new Course();
            course.Id = id;
            return course;
        }


        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "Smash tha like button and subscribe to Singletons";
    }
}
