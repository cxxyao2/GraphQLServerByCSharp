using GraphQLDemo2.API.Schema.Queries;
using HotChocolate.Data.Filters;

namespace GraphQLDemo2.API.Schema.Filters
{
    public class CourseFilterType : FilterInputType<CourseQueryType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseQueryType> descriptor)
        {
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }
    }
}
