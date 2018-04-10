using System.Collections;
using System.Collections.Generic;

namespace BashSoft.Contracts.Repo.Database
{
    public interface IRequester
    {
        void GetStudentsScoresFromCourse(string courseName, string username);

        void GetAllStudentsFromCourse(string courseName);

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);

        ISimpleOrderedBag<IStudent> GettAllStudentsSorted(IComparer<IStudent> cmp);
    }
}
