using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.DataLoaders;
using GraphQLDemo2.API.Schema.Mutations;
using GraphQLDemo2.API.Schema.Queries;
using GraphQLDemo2.API.Schema.Subscriptions;
using GraphQLDemo2.API.Services.Courses;
using GraphQLDemo2.API.Services.Instructors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<DataContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

var app = builder.Build();

using(IServiceScope scope=app.Services.CreateScope())
{
    IDbContextFactory<DataContext> contextFactory = 
        scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();

    using(DataContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}

app.UseRouting();

app.UseWebSockets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});


app.Run();
