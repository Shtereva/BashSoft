﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BashSoft.Models
{
    public class Course
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string name;
        private Dictionary<string, Student> studentsByName;

        public Course(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(this.name), ExceptionMessages.NullOrEmptyValue);
                }

                this.name = value;
            }
        }
        public IReadOnlyDictionary<string, Student> StudentsByName => this.studentsByName;
        public void EnrollStudent(Student student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, student.UserName, this.name));
            }

            this.studentsByName.Add(student.UserName, student);
        }
    }
}
