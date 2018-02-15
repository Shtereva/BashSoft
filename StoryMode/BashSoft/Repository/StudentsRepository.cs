using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BashSoft.Models;

namespace BashSoft
{
    public class StudentsRepository
    {
        private bool isDataInitialized = false;
        private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;
        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
            this.sorter = sorter;
            this.filter = filter;

        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitializedException);
                return;
            }
                
            this.students = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            OutputWriter.WriteMessageOnNewLine("Redaing data");
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;
        }

        public void GetStudentsScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName].studentsByName[username].marksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");

                foreach (var studentsMarkEntry in this.courses[courseName].studentsByName)
                {
                    this.GetStudentsScoresFromCourse(courseName, studentsMarkEntry.Key);
                }
            }
        }

        private void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                string pattern = @"(?<course>[A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+(?<username>[A-Za-z]+\d{2}_\d{2,4})\s(?<score>[\s0-9]+)";

                var regx = new Regex(pattern);

                var allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!String.IsNullOrEmpty(allInputLines[line]) && regx.IsMatch(allInputLines[line]))
                    {
                        var match = regx.Match(allInputLines[line]);
                        string courseName = match.Groups["course"].Value;
                        string studentName = match.Groups["username"].Value;
                        try
                        {
                            string scoresStr = match.Groups["score"].Value;

                            var scores = scoresStr
                                .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

                            if (scores.Any(s => s > 100 || s < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                            }

                            if (scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(studentName))
                            {
                                this.students.Add(studentName, new Student(studentName));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            var course = this.courses[courseName];
                            var student = this.students[studentName];

                            student.EnrollInCourse(course);
                            student.SetMarksInCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                       
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }

                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }

            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentsUserName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].studentsByName.ContainsKey(studentsUserName))
            {
                return true;
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }

            return false;
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].studentsByName.Count;
                }

                var marks = this.courses[courseName].studentsByName[courseName].marksByCourseName;
                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].studentsByName.Count;
                }

                var marks = this.courses[courseName].studentsByName[courseName].marksByCourseName;

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}
