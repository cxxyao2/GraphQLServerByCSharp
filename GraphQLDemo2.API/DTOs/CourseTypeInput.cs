using GraphQLDemo2.API.Entities;

namespace GraphQLDemo2.API.DTOs
{
    public class CourseTypeInput
    {  
        public string Name { get; set; }
        public Subject Subject { get; set; }

        public int InstructorId { get; set; }
    }
}
