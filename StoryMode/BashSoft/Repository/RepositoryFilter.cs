using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public class RepositoryFilter
    {
        public void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                FilterAndTake(wantedData, x => x >= 5, studentsToTake);
            }

            else if (wantedFilter == "average")
            {
                FilterAndTake(wantedData, x => x >= 3.5 && x < 5, studentsToTake);
            }

            else if (wantedFilter == "poor")
            {
                FilterAndTake(wantedData, x => x < 3.5, studentsToTake);
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter, int studentsToTake)
        {
            int counterForPrinted = 0;

            foreach (var data in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                double averageScore = data.Value.Average();
                double percentageOfFullfillment = averageScore / 100;
                double mark = percentageOfFullfillment * 4 + 2;

                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(data);
                    counterForPrinted++;
                }
            }
        }
    }
}
