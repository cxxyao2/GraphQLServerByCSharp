using GraphQLDemo2.API.DataLoaders;
using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;

namespace GraphQLDemo2.API.Schema.Queries
{
    public class CourseQueryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }

        [IsProjected(true)]
        public int InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorDTO?> InstructorMember([Service] InstructorDataLoader instructorDataLoader)
        {
            var instructor =  await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);

            return new InstructorDTO()
            { 
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName
            };
        }


        public List<Student> Students { get; set; }
    }
}
