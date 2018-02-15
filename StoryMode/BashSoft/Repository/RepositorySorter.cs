using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public class RepositorySorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();

            if (comparison == "ascending")
            {
                PrintStudents(studentsMarks
                    .OrderBy(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, y => y.Value));
            }

            else if (comparison == "descending")
            {
                PrintStudents(studentsMarks
                    .OrderByDescending(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, y => y.Value));
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsMarks)
        {
            foreach (var student in studentsMarks)
            {
                OutputWriter.PrintStudent(student);
            }
        }
    }
}
