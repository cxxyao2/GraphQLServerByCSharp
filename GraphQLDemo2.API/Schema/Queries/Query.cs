using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.Entities;
using GraphQLDemo2.API.Schema.Filters;
using GraphQLDemo2.API.Schema.Sorters;
using GraphQLDemo2.API.Services.Courses;

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
        /* public IEnumerable<Course> GetCourses() 
        {
            Random random = new Random();
            return Enumerable.Range(1, 5)
                .Select(e => new Course
                {
                    Id = random.Next(),
                    Name = $"Course {e}",
                    Subject = (Subject)random.Next(0, Enum.GetValues(typeof(Subject)).Length),
                    InstructorId = random.Next(),
                    Instructor = new Instructor
                    {
                        Id = random.Next(),
                        FirstName = $"Instructor {e}",
                        LastName = "Doe",
                        Salary = random.Next(50000, 100000)
                    },
                    Students = new List<Student>()
                })
                .ToArray();
        }
        */

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
