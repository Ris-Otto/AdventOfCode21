using System;
using System.IO;
using System.Linq;

namespace AOCP1
{
    public static class Challenge1
    {
        
        private static int count;

        public static void CompleteChallenge1(string path) {
            
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            
            while (!sr.EndOfStream) {
                AOC1(ReadThreeLines(sr), sr);
            }
            Console.WriteLine("\nPuzzle One! ---------------------->");
            Console.WriteLine(count);
        }

        private static void AOC1(int[] threeLines, StreamReader sr) {

            int A = threeLines.Sum();

            threeLines = IncReading(threeLines, sr);

            int B = threeLines.Sum();
            
            IncBasedOnComparison(A, B);
        }

        private static int[] ReadThreeLines(StreamReader sr) {
            int[] threeLines = new int[3];
            for (int i = 0; i < threeLines.Length; i++)
                threeLines[i] = int.Parse(sr.ReadLine());
            return threeLines;
        }

        private static void IncBasedOnComparison(int old, int neww) {
            if (neww <= old) return;
            count++;
        }

        private static int[] IncReading(int[] threelines, TextReader sr) {
            string newLine = sr.ReadLine();
            threelines[2] = threelines[1];
            threelines[1] = threelines[0];
            threelines[0] = int.Parse(newLine);
            return threelines;
        }
    }
}