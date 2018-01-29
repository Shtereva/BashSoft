using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                FilterAndTake(wantedData, ExcellentFilter, studentsToTake);
            }

            else if (wantedFilter == "average")
            {
                FilterAndTake(wantedData, AverageFilter, studentsToTake);
            }

            else if (wantedFilter == "poor")
            {
                FilterAndTake(wantedData, PoorFilter, studentsToTake);
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private static void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter, int studentsToTake)
        {
            int counterForPrinted = 0;

            foreach (var data in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                double averageMark = Average(data.Value);
                if (givenFilter(averageMark))
                {
                    OutputWriter.PrintStudent(data);
                    counterForPrinted++;
                }
            }
        }

        private static bool ExcellentFilter(double mark)
        {
            return mark >= 5.0;
        }

        private static bool AverageFilter(double mark)
        {
            return mark >= 3.5 && mark < 5.0;
        }

        private static bool PoorFilter(double mark)
        {
            return mark < 3.5;
        }

        private static double Average(List<int> scoresOnTasks)
        {
            double totalScore = 0;
            foreach (var score in scoresOnTasks)
            {
                totalScore += score;
            }

            double percentageOfAll = totalScore / (scoresOnTasks.Count * 100);

            double mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}
