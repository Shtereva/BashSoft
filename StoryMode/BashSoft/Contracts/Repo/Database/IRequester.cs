namespace BashSoft.Contracts.Repo.Database
{
    public interface IRequester
    {
        void GetStudentsScoresFromCourse(string courseName, string username);

        void GetAllStudentsFromCourse(string courseName);
    }
}
