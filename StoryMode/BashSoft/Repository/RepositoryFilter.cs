﻿using System;
using System.Collections.Generic;
using System.Linq;
using BashSoft.Contracts.Repo;

namespace BashSoft
{
    public class RepositoryFilter : IDataFilter
    {
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
            }

            else if (wantedFilter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 3.5 && x < 5, studentsToTake);
            }

            else if (wantedFilter == "poor")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 3.5, studentsToTake);
            }

            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter, int studentsToTake)
        {
            int counterForPrinted = 0;

            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(studentMark);
                    counterForPrinted++;
                }
            }
        }
    }
}
