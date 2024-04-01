namespace GraphQLDemo2.API.DTOs
{
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }

        public List<CourseDTO> Courses { get; set; }
    }
}
