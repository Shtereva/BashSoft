using System;
using System.Collections.Generic;
using BashSoft.Contracts;
using BashSoft.Contracts.Repo.Database;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class DisplayCommand : Command
    {
        public DisplayCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string entityToDisplay = this.Data[1];
            string orderType = this.Data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<IStudent> studentComparator = this.CreateStudentsComparator(orderType);
                ISimpleOrderedBag<IStudent> list = this.Repository.GettAllStudentsSorted(studentComparator);

                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }

            if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<ICourse> coursesComparator = this.CreateCoursesComparator(orderType);
                ISimpleOrderedBag<ICourse> list = this.Repository.GetAllCoursesSorted(coursesComparator);

                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
        }

        private IComparer<ICourse> CreateCoursesComparator(string orderType)
        {
            if (orderType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((x, y) => x.CompareTo(y));
            }

            if (orderType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((x, y) => y.CompareTo(x));
            }

            throw new InvalidCommandException(this.Input);
        }
        private IComparer<IStudent> CreateStudentsComparator(string orderType)
        {
            if (orderType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((x, y) => x.CompareTo(y));
            }

            if (orderType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((x, y) => y.CompareTo(x));
            }

            throw new InvalidCommandException(this.Input);
        }
    }
}
