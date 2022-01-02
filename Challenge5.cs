using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCP1
{
    public static class Challenge5
    {
        private const int SIZE = 1000;
        public static void CompleteChallenge5(string path) {

            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            char[] startEndSplitter = { ',' };
            char[] LineSplitter = { '-', '>' };
            List<Line> lines = new List<Line>();


            while (!sr.EndOfStream) {
                string currLine = sr.ReadLine();
                string[] tuples = currLine.Split(LineSplitter).RemoveWhiteSpace();
                int[] ints = new int[4];
                int idx = 0;
                foreach (string tuple in tuples) {
                    foreach (string s in tuple.Split(startEndSplitter)) {
                        ints[idx++] = int.Parse(s);
                    }
                }
                lines.Add(new Line(ints));
            }
            
            int[,] grid = LayLines(lines);
            Console.WriteLine("\nPuzzle Five! ---------------------->");
            Console.WriteLine(CheckGrid(grid));
        }

        private static int CheckGrid(int[,] grid) {
            return grid.Cast<int>().Count(i => i > 1);
        }

        private static void PrintGrid(int[,] board) {
            for (int i = 0; i < board.GetLength(1); i++) {
                for (int j = 0; j < board.GetLength(0); j++) {
                    Console.Write(board[j, i] + " ");
                }
                Console.WriteLine();
            }
        }

        private static int[,] CreateGrid() {
            return new int[SIZE, SIZE];
        }

        private static int[,] LayLines(List<Line> lines) {
            
            int[,] grid = CreateGrid();
            
            List<Line> H_V_lines = lines.Except(lines.Where(line => !line.CompareInternal(line.Start, line.End))).ToList();
            List<Line> D_lines = lines.Except(H_V_lines).ToList();
            
            return LayHAndVLines(H_V_lines, LayDLines(D_lines, grid));
        }

        private static int[,] LayDLines(List<Line> lines, int[,] grid) {
            foreach (Line line in lines) {
            
                int endX = line.End.Item1, endY = line.End.Item2, startX = line.Start.Item1, startY = line.Start.Item2;
                if (line.deltaX < 0) {//if line goes left
                    if (line.deltaY < 0) {//if line goes down
                        while (startX >= endX) {
                            grid[startX--, startY--]++;
                        }
                    }
                    else {//if line goes up
                        while (startX >= endX) {
                            grid[startX--, startY++]++;
                        }
                    }
                    
                }
                else {
                    if (line.deltaY < 0) {//if line goes down
                        while (endX >= startX) {
                            grid[startX++, startY--]++;
                        }
                    }
                    else {//if line goes up
                        while (endX >= startX) {
                            grid[startX++, startY++]++;
                        }
                    }
                }
            }
            return grid;
        }

        private static int[,] LayHAndVLines(List<Line> lines, int[,] grid) {
            foreach (Line line in lines) {
                
                int endX = line.End.Item1, endY = line.End.Item2, startX = line.Start.Item1, startY = line.Start.Item2;
                int greater = line.ReturnGreater(startX, endX, out int lesserX);
                int lesser = lesserX;
                int otherIdx = startY;
                
                if (greater == lesser) {
                    greater = line.ReturnGreater(startY, endY, out int lesserY);
                    lesser = lesserY;
                    otherIdx = lesserX;
                    while (greater >= lesser) {
                        grid[otherIdx, greater--]++;
                        
                    }
                }

                while (greater >= lesser) {
                    grid[greater--, otherIdx]++;
                }

            }
            return grid;
        }

        private static string[] RemoveWhiteSpace(this IEnumerable<string> strings) {
            return strings.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
    }

    public class Line
    {
        public Line(int[] whole) {
            Start = new Tuple<int, int>(whole[0], whole[1]);
            End = new Tuple<int, int>(whole[2], whole[3]);
            deltaX = (End.Item1 - Start.Item1);
            deltaY = (End.Item2 - Start.Item2);
        }

        public void PrintLine() {
            Console.WriteLine(Start.Item1 + "," + Start.Item2 + " -> " + End.Item1 + "," + End.Item2);
        }

        public int deltaX { get; }
        
        public int deltaY { get; }

        public Tuple<int, int> Start { get; }
        public Tuple<int, int> End { get; }

        public bool CompareInternal(Tuple<int, int> s, Tuple<int, int> e) {
            (int item1, int item2) = s;
            (int item3, int item4) = e;
            if (item1 == item3) return true;
            return item2 == item4;
        }

        public int ReturnGreater(int start, int end, out int lesser) {
            int ret;
            if (start == end) {
                lesser = start;
                return end;
            }
            if (start > end) {
                ret = start;
                lesser = end;
            } else {
                ret = end;
                lesser = start;
            }
            return ret;
        }

        
    }
}