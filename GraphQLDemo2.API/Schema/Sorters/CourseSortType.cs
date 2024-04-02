using GraphQLDemo2.API.Schema.Queries;
using HotChocolate.Data.Sorting;

namespace GraphQLDemo2.API.Schema.Sorters
{
    public class CourseSortType : SortInputType<CourseQueryType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseQueryType> descriptor)
        {
            // descriptor.Ignore(c => c.Id);
            // descriptor.Ignore(c => c.InstructorId);
            descriptor.Field(c => c.Name).Name("CourseName");

            base.Configure(descriptor);
        }
    }
}
