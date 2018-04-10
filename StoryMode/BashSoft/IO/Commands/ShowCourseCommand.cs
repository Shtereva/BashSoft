using BashSoft.Contracts.Repo.Database;
using BashSoft.Exceptions;
using BashSoft.IO.Attributes;

namespace BashSoft.IO.Commands
{
    [Alias("show")]
    public class ShowCourseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ShowCourseCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                this.repository.GetAllStudentsFromCourse(this.Data[1]);

            }

            else if (this.Data.Length == 3)
            {
                this.repository.GetStudentsScoresFromCourse(this.Data[1], this.Data[2]);
            }

            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
