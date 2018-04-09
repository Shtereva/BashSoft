﻿using BashSoft.Contracts;
using BashSoft.Contracts.Repo.Database;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1)
            {
                this.InputOutputManager.TraverseDirectory(0);
            }

            else if (this.Data.Length == 2)
            {
                int depth = 0;
                bool hasParsed = int.TryParse(this.Data[1], out depth);

                if (hasParsed)
                {
                    this.InputOutputManager.TraverseDirectory(depth);
                }

                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }

            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
