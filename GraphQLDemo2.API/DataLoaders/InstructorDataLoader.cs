using GraphQLDemo2.API.Entities;
using GraphQLDemo2.API.Services.Instructors;

namespace GraphQLDemo2.API.DataLoaders
{
    public class InstructorDataLoader: BatchDataLoader<int, Instructor>
    {
        private readonly InstructorsRepository  _instructorsRepository;
        public InstructorDataLoader(
            InstructorsRepository repo,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options
            ): base(batchScheduler,options)
        {
            _instructorsRepository = repo;
        
        }

        protected override async Task<IReadOnlyDictionary<int, Instructor>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken ct) 
        {
            IEnumerable<Instructor> instructors = await _instructorsRepository.GetManyByIds(keys);

            return instructors.ToDictionary(i => i.Id);
        }
    }
}
