namespace GraphQLDemo2.API.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double GPA { get; set; }

        public List<CourseDTO> Courses { get; set; }
    }
}
