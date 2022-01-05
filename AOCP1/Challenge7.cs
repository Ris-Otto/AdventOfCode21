using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using static System.Int32;

namespace AOCP1
{
    public static class Challenge7
    {

        public static void CompleteChallenge7(string path) {

            StreamReader sr = ChallengeRunner.ReadFile(path);
            Stopwatch sw = new Stopwatch();
            
            List<int> subs = sr.ReadLine()
                ?.
                Split(char.Parse(",")).
                Select(Parse).ToList();
            
            
            Console.WriteLine("\nPuzzle Seven! ---------------------->");
            Console.WriteLine(OptimalValue(subs, true));
            Console.WriteLine(OptimalValue(subs, false));
        }

        

        private static (int, int) OptimalValue(List<int> subs, bool part1) {

            int range = subs.Max() - subs.Min();
            Dictionary<int, int> optimal = new Dictionary<int, int>();

            for (int j = 0; j < range; j++) 
                optimal.Add(j,
                    part1
                        ? subs.Sum(x => Math.Abs(x - j))
                        : subs.Sum(x => CalculateIndividualFuelCost(Math.Abs(x - j))));
            
            
            int optimalValue = optimal.Values.Min();
            for (int i = 0; i < range; i++) {
                if (optimal[i] != optimalValue) continue;
                optimalValue = i;
                break;
            }
            return (optimalValue , optimal[optimalValue]);
        }

        private static int CalculateIndividualFuelCost(int distance) {
            //sum 1+2+3+...+n
            return distance*(distance+1)/2;
        }

    }
}