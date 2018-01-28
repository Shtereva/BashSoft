using System;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData(string fileName)
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Redaing data");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }

            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        public static void GetStudentsScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(username, studentsByCourse[courseName][username]));
            }
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");

                foreach (var kvp in studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(kvp);
                }
            }
        }

        private static void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]))
                    {
                        var tokens = allInputLines[line].Split(' ');
                        string course = tokens[0];
                        string student = tokens[1];
                        int mark = int.Parse(tokens[2]);

                        if (!studentsByCourse.ContainsKey(course))
                        {
                            studentsByCourse[course] = new Dictionary<string, List<int>>();
                        }

                        if (!studentsByCourse[course].ContainsKey(student))
                        {
                            studentsByCourse[course][student] = new List<int>();
                        }

                        studentsByCourse[course][student].Add(mark);
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

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }

                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
                }

            }

            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private static bool IsQueryForStudentPossiblе(string courseName, string studentsUserName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentsUserName))
            {
                return true;
            }

            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
            }

            return false;
        }
    }
}
