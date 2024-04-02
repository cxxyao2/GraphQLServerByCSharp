using GraphQLDemo2.API.Entities;

namespace GraphQLDemo2.API.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        
        public int InstructorId { get; set; }

        [GraphQLNonNullType]
        public InstructorDTO Instructor { get; set; }

        public IEnumerable<StudentDTO> Students { get; set; }

     
    }
}
