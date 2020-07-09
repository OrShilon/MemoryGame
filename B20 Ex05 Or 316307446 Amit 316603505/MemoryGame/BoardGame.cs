using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class BoardGame
    {
        public Square[,] m_SuqaresValue;

        public BoardGame(int i_Rows, int i_Columns)
        {
            this.NumOfRows = i_Rows;
            this.NumOfColumns = i_Columns;
            this.m_SuqaresValue = randomSquaresBuilder(NumOfRows, NumOfColumns);
        }

        public int NumOfRows { get; }

        public int NumOfColumns { get; }

        public StringBuilder BuildBoardGame()
        {
            StringBuilder board = new StringBuilder();
            char columnIndexes = 'A';

            // building board game.
            board.Append("    ");
            for (int i = 0; i < NumOfColumns; i++)
            {
                board.Append(columnIndexes + "   ");
                columnIndexes++;
            }

            board.Append(Environment.NewLine);
            board.Append("  =");
            board.AppendFormat("{0}", new string('=', 4 * NumOfColumns));
            board.Append(Environment.NewLine);
            for (int i = 0; i < NumOfRows; i++)
            {
                board.Append((i + 1) + " |");
                for (int j = 0; j < NumOfColumns; j++)
                {
                    if (m_SuqaresValue[i, j].visible)
                    {
                        board.AppendFormat(" {0} ", m_SuqaresValue[i, j].letter);
                    }
                    else
                    {
                        board.Append("   ");
                    }

                    board.Append("|");
                }

                board.Append(Environment.NewLine);
                board.Append("  =");
                board.AppendFormat("{0}", new string('=', 4 * NumOfColumns));
                board.Append(Environment.NewLine);
                columnIndexes++;
            }

            return board;
        }


        private Square[,] randomSquaresBuilder(int i_NumOfRows, int i_NumOfColumns)
        {
            Square[,] lettersMatrix = new Square[i_NumOfRows, i_NumOfColumns];
            char currentLetter = 'G';
            int currentLetterIterator = 0;

            // initialize the martix with letters
            for (int i = 0; i < NumOfRows; i++)
            {
                for (int j = 0; j < NumOfColumns; j++)
                {
                    lettersMatrix[i, j] = new Square(currentLetter);
                    currentLetterIterator++;
                    if (currentLetterIterator % 2 == 0)
                    {
                        currentLetter++;
                    }
                }
            }

            matrixRandomizer(ref lettersMatrix);

            return lettersMatrix;
        }

        private void matrixRandomizer<T>(ref T[,] io_LetterArray)
        {
            Random rand = new Random();
            int totalNumberOfCells = NumOfColumns * NumOfRows;

            // randomize the matrix
            for (int i = 0; i < io_LetterArray.Length; i++)
            {
                int toSwap = rand.Next(i, totalNumberOfCells);

                int row_CurrentPosition = i / NumOfColumns;
                int col_CurrentPosition = i % NumOfColumns;
                int row_NewPosition = toSwap / NumOfColumns;
                int col_NewPosition = toSwap % NumOfColumns;

                T swap = io_LetterArray[row_CurrentPosition, col_CurrentPosition];
                io_LetterArray[row_CurrentPosition, col_CurrentPosition] = io_LetterArray[row_NewPosition, col_NewPosition];
                io_LetterArray[row_NewPosition, col_NewPosition] = swap;
            }
        }

        public string PrintBoardGame()
        {
            return BuildBoardGame().ToString();
        }
    }
}
