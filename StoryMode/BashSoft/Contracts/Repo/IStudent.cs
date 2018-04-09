using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IStudent
    {
        string UserName { get; }

        IReadOnlyDictionary<string, ICourse> ErolledCourses { get; }

        IReadOnlyDictionary<string, double> MarksByCourseName { get; }

        void EnrollInCourse(ICourse course);

        void SetMarksInCourse(string courseName, params int[] scores);
    }
}
