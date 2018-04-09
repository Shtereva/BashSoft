using System.Collections.Generic;

namespace BashSoft.Contracts.Repo
{
    public interface IDataSorter
    {
        void OrderAndTake(Dictionary<string, double> studentsMarks, string comparison, int studentsToTake);
    }
}
