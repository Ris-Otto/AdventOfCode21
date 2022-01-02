using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOCP1
{
    public static class Challenge6
    {

        private const int DAYS = 256;
        private const int LANTERN_SPAWN = 7;
        private const int NEW_LANTERN = 9;
        
        public static void CompleteChallenge6(string path) {
            
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            List<int> lifeSpans = sr.ReadLine()
                ?.
                Split(char.Parse(",")).
                Select(s => (int.Parse(s))).ToList();
            Dictionary<int, long> spawnedDict = new Dictionary<int, long>();
            if (lifeSpans != null)
                for (int index = 0; index < lifeSpans.Count; index++) {
                    int amount = lifeSpans.Count(s => s == index);
                    spawnedDict.Add(index, amount);
                }
            else {
                Console.WriteLine("nice input loser");
            }
            
            Console.WriteLine("\nPuzzle Six! ---------------------->");
            Console.WriteLine("Lanternfish count: " + Simulate(spawnedDict));
        }

        private static long Simulate(Dictionary<int, long> spawnedDict) {
            for (int i = 1; i < DAYS; i++) {
                Dictionary<int, long> newFish = new Dictionary<int, long>();
                foreach (KeyValuePair<int, long> kvp in spawnedDict.Where(kvp => kvp.Key == i)) {
                    newFish.Add(i + NEW_LANTERN, kvp.Value);
                    newFish.Add(kvp.Key + LANTERN_SPAWN, kvp.Value);
                }
                
                spawnedDict.Remove(i);
                
                foreach (KeyValuePair<int, long> kvp in newFish) 
                    if (spawnedDict.ContainsKey(kvp.Key)) 
                        spawnedDict[kvp.Key] += kvp.Value;
                    else 
                        spawnedDict.Add(kvp.Key, kvp.Value);
            }
            
            spawnedDict = spawnedDict.Where(
                kvp => kvp.Value != 0).ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value);
            
            PrintDict(spawnedDict);
            return spawnedDict.Values.Sum();
        }

        private static void PrintDict(Dictionary<int, long> dict) {
            foreach (KeyValuePair<int, long> VARIABLE in dict) 
                Console.WriteLine(VARIABLE.Key + " : " + VARIABLE.Value);
        }
    }
}