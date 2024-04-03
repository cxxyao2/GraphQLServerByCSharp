using GraphQLDemo2.API.Entities;

namespace GraphQLDemo2.API.Schema.Queries
{
    public class InstructorQueryType: ISearchResultType
    {   
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public double Salary { get; set; }

    }
}
