namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main()
        {
            StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsFromCourse("Unity");
            StudentsRepository.GetStudentsScoresFromCourse("Unity", "Ivan");
        }
    }
}
