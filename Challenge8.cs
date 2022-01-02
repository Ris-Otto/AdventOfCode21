using System;
using System.IO;

namespace AOCP1
{
    public static class Challenge8
    {
        
        
        public static void CompleteChallenge8(string path) {
            
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            
            
            Console.WriteLine("\nPuzzle Eight! ---------------------->");
            
        }
        
        
    }
}