﻿using System;
using System.Collections.Generic;
using System.Linq;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get => this.userName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException(nameof(this.userName));
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, Course> eErolledCourses => this.enrolledCourses;
        public IReadOnlyDictionary<string, double> MarksByCourseName => this.marksByCourseName;

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException( this.UserName, course.Name);
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException();
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExams =
                scores.Sum() / (double) (Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);

            double mark = percentageOfSolvedExams * 4 + 2;
            return mark;
        }
    }
}
