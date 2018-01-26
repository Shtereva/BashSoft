namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main()
        {
            StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsFromCourse("");
            StudentsRepository.GetStudentsScoresFromCourse("", "");
        }
    }
}
