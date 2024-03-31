namespace GraphQLDemo2.API.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public List<Student> Students { get; set; }
    }
}
