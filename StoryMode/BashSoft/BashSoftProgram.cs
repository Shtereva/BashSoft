namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main()
        {
            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCourse("Unity");
            //StudentsRepository.GetStudentsScoresFromCourse("Unity", "Ivan");

            //IOManager.CreateDirectoryInCurrentFolder("My Folder");

            //IOManager.ChangeCurrentDirectoryRelative(@"..");


            // IOManager.ChangeCurrentDirectoryAbsolute(@"..");
            //IOManager.TraverseDirectory(0); // depth

            //IOManager.CreateDirectoryInCurrentFolder("*d");
            
            InputReader.StartReadingCommands();
        }
    }
}
