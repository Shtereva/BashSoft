using System.Collections.Generic;

namespace BashSoft.Contracts.Repo
{
    public interface IDataFilter
    {
        void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake);
    }
}
