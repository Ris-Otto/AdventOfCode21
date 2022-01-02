using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCP1
{
    public static class Challenge4
    {
        private const int SIZE = 5;
        public static void CompleteChallenge4(string path) {
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);
            const char splitter = ',';
            
            string[] bingoNumbers = sr.ReadLine().Split(splitter);
            List<string> bn = bingoNumbers.ToList();
            List<Board> boardsList = new List<Board>();

            while (!sr.EndOfStream) {
                boardsList.Add(new Board(ReadFive(sr)));
            }

            foreach (string t in bn) {
                foreach (Board b in boardsList)
                    MarkBoard(b, int.Parse(t));
                List<Board> kaki = boardsList.Where(FindWinner).ToList();

                if (boardsList.Count == 1) {
                    Console.WriteLine("\nPuzzle Four! ---------------------->");
                    PrintAnswer(boardsList[0], int.Parse(t));
                    break;
                }
                boardsList = boardsList.Except(kaki).ToList();
            }

        }

        private static bool FindWinner(Board b) {
            for (int i = 0; i < 5; i++) {
                int[] column = b.GetColumn(b.Markings, i, b.Markings.GetLength(1)).ToArray();
                int[] row = b.GetRow(b.Markings, i, b.Markings.GetLength(0)).ToArray();
                int sumColumn = column.Sum();
                int sumRow = row.Sum();
                if (sumColumn != SIZE && sumRow != SIZE) continue;
                b.winner = true;
                break;
            }
            return b.winner;
        }

        private static void PrintAnswer(Board b, int drawn) {
            
            List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
            
            for (int i = 0; i < b.Markings.GetLength(0); i++) 
                for(int j = 0; j < b.Markings.GetLength(1); j++)
                    if (b.Markings[i,j] != 1) 
                        indices.Add(new Tuple<int, int>(i,j));
            
            int sumOfUnmarked = indices.Sum(ij => b.boardRep[ij.Item1, ij.Item2]);
            Console.WriteLine(drawn*sumOfUnmarked);
        }

        private static int[,] ReadFive(TextReader sr) {
            string[] ret = new string[5];
            for (int i = 0; i < ret.Length; i++) {
                string curr = sr.ReadLine();
                if (curr.Length == 0) {
                    i--;
                    continue;
                }
                ret[i] = curr;
            }
            return BoardAsInts(ret);
        }

        private static int[,] BoardAsInts(string[] input) {
            const char splitter = ' ';
            int[,] boardRet = new int[5, 5];
            int row = 0;
            foreach (string s in input) {
                string[] split = s.Split(splitter).Where(s1 => !string.IsNullOrEmpty(s1)).ToArray();

                for (int column = 0; column < split.Length; column++)
                    boardRet[row, column] = int.Parse(split[column]);

                row++;
            }
            return boardRet;
        }
        
        private static void PrintBoard(int[,] board) {
            for (int i = 0; i < board.GetLength(0); i++) {
                for (int j = 0; j < board.GetLength(1); j++) {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void MarkBoard(Board b, int drawn) {
            for (int i = 0; i < b.boardRep.GetLength(0); i++) {
                for (int j = 0; j < b.boardRep.GetLength(1); j++) {
                    if(b.boardRep[i,j] != drawn) continue;
                    b.Markings[i, j] = 1;
                }
            }
        }
    }
    
    

    public class Board
    {
        
        public readonly int[,] boardRep;
        public int[,] Markings { get; }
        public bool winner;

        public Board(int[,] boardRep) {
            this.boardRep = boardRep;
            Markings = new int[5,5];
            Markings.Initialize();
        }

        public int[] GetColumn(int[,] matrix, int columnNumber, int matrixDimensionLength) {
            
            int[] ret = new int[(matrixDimensionLength)];
            
            for (int i = 0; i < matrixDimensionLength; i++) 
                ret[i] = (matrix[columnNumber, i]);
            
            return ret;
        }

        public int[] GetRow(int[,] matrix, int rowNumber, int matrixDimensionLength) {
            int[] ret = new int[(matrixDimensionLength)];
            
            for (int i = 0; i < matrixDimensionLength; i++) 
                ret[i] = (matrix[rowNumber, i]);
            
            return ret;
        }

        
        
    }
}