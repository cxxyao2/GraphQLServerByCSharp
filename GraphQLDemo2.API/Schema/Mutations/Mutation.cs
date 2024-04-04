using FluentValidation.Results;
using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;
using GraphQLDemo2.API.Schema.Subscriptions;
using GraphQLDemo2.API.Services.Courses;
using GraphQLDemo2.API.Validators;
using HotChocolate.Subscriptions;

namespace GraphQLDemo2.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly CourseTypeInputValidator _courseTypeInputValidator;

        public Mutation(CoursesRepository coursesRepository, CourseTypeInputValidator courseTypeInputValidator)
        {
            _coursesRepository = coursesRepository;
            _courseTypeInputValidator = courseTypeInputValidator;
        }

        public async Task<Course> CreateCourse(CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            Validate(courseInput);
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            Course course = await _coursesRepository.Create(courseDTO);

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;

        }

        private void Validate(CourseTypeInput courseInput)
        {
            ValidationResult validationResult = _courseTypeInputValidator.Validate(courseInput);
            if (!validationResult.IsValid)
            {
                throw new GraphQLException("Invalid input.");
            }
        }

        public async Task<Course> UpdateCourse(int id, CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            Validate(courseInput);

            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            Course course = await _coursesRepository.Update(courseDTO);

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";

            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;

        }

        public async Task<bool> DeleteCourse(int id)
        {
            return await _coursesRepository.Delete(id);
        }


    }
}
