using System;

namespace SimpleJudge
{
    public class StartUp
    {
        public static void Main()
        {
            Tester.CompareContent(@"actualPath", @"expectedPath");
        }
    }
}
