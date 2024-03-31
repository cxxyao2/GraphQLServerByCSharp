namespace GraphQLDemo2.API.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double GPA { get; set; }

        public List<Course> Courses { get; set; }
    }
}
