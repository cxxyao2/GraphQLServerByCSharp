using GraphQLDemo2.API.Data;
using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo2.API.Services.Instructors
{
    public class InstructorsRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public InstructorsRepository(IDbContextFactory<DataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructors()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.ToListAsync();
            }
        }

        public async Task<Instructor?> GetById(int id)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.FirstOrDefaultAsync(c => c.Id == id);
            }
        }

        public async Task<IEnumerable<Instructor>> GetManyByIds(IReadOnlyList<int> instructorIds)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                return await context.Instructors.Where(c => instructorIds.Contains(c.Id)).ToListAsync();
            }
        }
    }
}
