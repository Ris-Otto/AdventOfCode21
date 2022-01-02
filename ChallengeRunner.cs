using System;
using System.IO;
using static System.IO.Path;
using System.Net;
using static AOCP1.Challenge1;
using static AOCP1.Challenge3;
using static AOCP1.Challenge4;
using static AOCP1.Challenge5;
using static AOCP1.Challenge6;
using static AOCP1.Challenge7;
using static AOCP1.Challenge8;

namespace AOCP1
{
    public class ChallengeRunner
    {
        
        

        public static void Main() {

            DirectoryInfo workingDir = new DirectoryInfo(@"AOCP1/InputFiles");

            
            //...yea...
            string curr = new FileInfo(workingDir + @"/puzzle3.txt").FullName;
            string puzzle3 = curr;
            curr = new FileInfo(workingDir + @"/puzzle1.txt").FullName;
            string puzzle1 = curr;
            curr = new FileInfo(workingDir + @"/puzzle2.txt").FullName;
            string puzzle2 = curr; // apparently I deleted it xd
            curr = new FileInfo(workingDir + @"/puzzle4.txt").FullName;
            string puzzle4 = curr;
            curr = new FileInfo(workingDir + @"/puzzle5.txt").FullName;
            string puzzle5 = curr;
            curr = new FileInfo(workingDir + @"/puzzle6.txt").FullName;
            string puzzle6 = curr;
            curr = new FileInfo(workingDir + @"/puzzle7.txt").FullName;
            string puzzle7 = curr;
            curr = new FileInfo(workingDir + @"/test.txt").FullName;
            string puzzle8 = curr;
            

            CompleteChallenge1(puzzle1);
            CompleteChallenge3(puzzle3);
            CompleteChallenge4(puzzle4);
            CompleteChallenge5(puzzle5);
            CompleteChallenge6(puzzle6);
            CompleteChallenge7(puzzle7);
            //CompleteChallenge8(puzzle8);

        }

        public static StreamReader ReadFile(string path) {
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            return sr;
        }
    }
}