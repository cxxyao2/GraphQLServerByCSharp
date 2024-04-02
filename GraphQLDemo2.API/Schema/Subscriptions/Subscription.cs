using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo2.API.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public Course CourseCreated([EventMessage] Course course) => course;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<Course>> CourseUpdated(int courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(CourseUpdated)}";
            return topicEventReceiver.SubscribeAsync<Course>(topicName);
        }



    }
}
