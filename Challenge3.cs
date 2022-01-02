using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCP1
{
    public static class Challenge3
    {

        public static void CompleteChallenge3(string path) {
            
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            List<string> culled = new List<string>();
            string currLine = sr.ReadLine();
            if (currLine == null) return;
            int[] mostCommon = new int[currLine.Length];
            
            while (!sr.EndOfStream) {
                if (currLine == null) continue;
                for (int i = 0; i < currLine.Length; i++)
                    mostCommon.ReadBits(currLine, i);
                culled.Add(currLine);
                currLine = sr.ReadLine();
            }

            long OGR = BinaryAsDecimalToDecimal(
                long.Parse(
                    MostCommonRecursive(mostCommon, culled.ToArray(), true)[0]));
            long CO2_SR = BinaryAsDecimalToDecimal(
                long.Parse(
                    MostCommonRecursive(mostCommon, culled.ToArray(), false)[0]));

            
            Console.WriteLine("\nPuzzle Three! ---------------------->");
            Console.WriteLine(CO2_SR * OGR);
        }

        private static string[] MostCommonRecursive(int[] occurrences, string[] fileInput, bool filterMostCommon, int currentBit = 0) {
            
            //Default
            if (fileInput.Length <= 1) return fileInput;

            {//Recursion
                
                //Set array elements to either 1 or 0 depending on which occurred most in that position
                occurrences.SetMostCommonBits(filterMostCommon, currentBit);

                //Invalid lines
                List<string> toRemove = fileInput
                    .Where(line => !line[currentBit].Equals(char.Parse(occurrences[currentBit].ToString()))).ToList();

                //Remove invalid bit sequences
                fileInput = fileInput.Except(toRemove).ToArray();

                //Increment bit to check
                currentBit++;
                
                //Update occurrences according to remaining bit sequences and go agane
                occurrences.UpdateMostCommon(fileInput, currentBit);
                
                return MostCommonRecursive(occurrences, fileInput, filterMostCommon, currentBit);
            }
        }

        private static void UpdateMostCommon(this IList<int> occurrences, IEnumerable<string> input, int startBit) {
            ResetArray(occurrences); //Reset array so that the values aren't corrupted by previous values
            foreach (string line in input) 
                for(int bitPos = startBit; bitPos < occurrences.Count; bitPos++)
                    occurrences.ReadBits(line, bitPos);
        }

        private static void SetMostCommonBits(this IList<int> occurrences, bool filterMostCommon, int startBit) {
            for (int j = startBit; j < occurrences.Count; j++) 
                if(filterMostCommon)
                    occurrences[j] = occurrences[j] >= 0 ? 1 : 0;
                else 
                    occurrences[j] = occurrences[j] >= 0 ? 0 : 1;
        }

        private static long BinaryAsDecimalToDecimal(long binary) {
            
            long decimalValue = 0;
            int base1 = 1;
            
            while (binary > 0) {
                long reminder = binary % 10;
                binary /= 10;
                decimalValue += reminder * base1;
                base1 *= 2;
            }
            
            return decimalValue;
        }

        private static void ReadBits(this IList<int> array, string line, int bitPos) {
            //aka if there are more ones the element is a positive integer, more zeroes -> element is negative
            if (line[bitPos].Equals('1'))
                array[bitPos] += 1;
            else array[bitPos] -= 1;
        }

        private static void ResetArray(IList<int> array) {
            for (int i = 0; i < array.Count; i++) 
                array[i] = 0;
        }
    }
}