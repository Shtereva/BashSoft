﻿using BashSoft.Contracts;
using BashSoft.Contracts.Repo.Database;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ChangeAbsolutePathCommand : Command
    {
        public ChangeAbsolutePathCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string absPath = this.Data[1];
            this.InputOutputManager.ChangeCurrentDirectoryAbsolute(absPath);
        }
    }
}
