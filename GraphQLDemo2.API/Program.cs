using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.Schema;
using GraphQLDemo2.API.Services.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<DataContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CoursesRepository>();

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
