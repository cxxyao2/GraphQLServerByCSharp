namespace GraphQLDemo2.API.Schema
{
    public class Query
    {
        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "Smash tha like button and subscribe to Singletons";
    }
}
