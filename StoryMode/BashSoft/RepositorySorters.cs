using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public static class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();

            if (comparison == "ascending")
            {
                OrderAndTake(wantedData, studentsToTake, CompareInOrder);
            }

            else if (comparison == "descending")
            {
                OrderAndTake(wantedData, studentsToTake, CompareInDescendigOrder);
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static void OrderAndTake(Dictionary<string, List<int>> wantedData, int studentsToTake,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        {
            var studentsSorted = new Dictionary<string, List<int>>(GetSortedStudents(wantedData, studentsToTake, comparisonFunc));

            foreach (var student in studentsSorted)
            {
                OutputWriter.PrintStudent(student);
            }
        }

        private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> studentsWanted, int takeCount,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> Comparison)
        {
            int valuesTaken = 0;
            var studentsSorted = new Dictionary<string, List<int>>();

            var nextInOrder = new KeyValuePair<string, List<int>>();
            bool isSorted = false;

            while (valuesTaken < takeCount)
            {
                isSorted = true;

                foreach (var studentWithScore in studentsWanted)
                {
                    if (!string.IsNullOrEmpty(nextInOrder.Key))
                    {
                        int comparisonResult = Comparison(studentWithScore, nextInOrder);

                        if (comparisonResult >= 0 && !studentsSorted.ContainsKey(studentWithScore.Key))
                        {
                            nextInOrder = studentWithScore;
                            isSorted = false;
                        }
                    }

                    else
                    {
                        if (!studentsSorted.ContainsKey(studentWithScore.Key))
                        {
                            nextInOrder = studentWithScore;
                            isSorted = false;
                        }
                    }
                }

                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    valuesTaken++;
                    nextInOrder = new KeyValuePair<string, List<int>>();
                }
            }

            return studentsSorted;
        }
        private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = firstValue.Value.Sum();
            int totalOfSecondMarks = secondValue.Value.Sum();

            return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        }

        private static int CompareInDescendigOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = firstValue.Value.Sum();
            int totalOfSecondMarks = secondValue.Value.Sum();

            return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        }
    }
}
