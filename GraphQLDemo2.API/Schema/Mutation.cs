﻿using GraphQLDemo2.API.DTOs;
using GraphQLDemo2.API.Entities;
using GraphQLDemo2.API.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLDemo2.API.Schema
{
    public class Mutation
    {
        private readonly CoursesRepository _coursesRepository;

        public Mutation(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Course> CreateCourse(CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

           Course course =  await _coursesRepository.Create(courseDTO);

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;

        }

        public async Task<Course> UpdateCourse(int id, CourseTypeInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
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
