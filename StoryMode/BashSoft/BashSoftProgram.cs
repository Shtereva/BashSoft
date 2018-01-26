using System;

namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main(string[] args)
        {
            StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsFromCourse("Unity");
            StudentsRepository.GetStudentsScoresFromCourse("Unity", "Ivan");
        }
    }
}
