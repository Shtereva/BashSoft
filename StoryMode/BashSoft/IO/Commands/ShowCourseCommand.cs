﻿using BashSoft.Contracts;
using BashSoft.Contracts.Repo.Database;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                this.Repository.GetAllStudentsFromCourse(this.Data[1]);

            }

            else if (this.Data.Length == 3)
            {
                this.Repository.GetStudentsScoresFromCourse(this.Data[1], this.Data[2]);
            }

            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
