using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Minesweeper
    {
        public bool hasStarted { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private bool[,] Board { get; set; }
        Random random = new Random();


        public Minesweeper(int row, int column, int numOfMines)
        {
            this.Rows = row;
            this.Columns = column;

            // Determine the size of the board
            Board = new bool[Rows, Columns];

            // Initialize the board by setting all tiles to false
            InitializeBoard();

            // Determine and set the tiles with mines
            DetermineMines(numOfMines);
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Board[i, j] = false;
                }
            }
        }

        private void DetermineMines(int numOfMines)
        {
            var i = 0;
            do
            {
                if (SetTileToMine())    // Calls to set a specific tile to a mine
                    i++;                // Increments if a mine has been planted successfully
            } while (i < numOfMines);
        }

        // Sets a tile to a mine
        private bool SetTileToMine()
        {
            var tile = random.Next(Rows * Columns);

            int row = tile / Columns;
            int col = tile % Columns;

            if (Board[row, col] == false)   // Checks whether the tile is already a mine
            {
                Board[row, col] = true;     // Returns true if a mine has been planted successfully
                return true;
            }
            else
            {
                return false;               // Returns false if the tile has already a mine
            }
        }

        // Is called to determine if the tile is a Mine
        public bool IsMine(int row, int column)
        {
            return Board[row, column];
        }

        // Called to determine the number of mines surrounding a specific tile
        public int DisplayTileContent(int row, int column)
        {
            var num = 0;
            for (int i = row - 1; i <= row + 1; i++)     // Scans rows above, mid and below
            {
                if (i >= 0 && i < Board.GetLength(0))   // Checks for top and bottom rows
                {
                    for (int j = column - 1; j <= column + 1; j++)   // Scans for left, mid and right columns
                    {
                        if ((j >= 0 && j < Board.GetLength(1)) && (i != row || j != column)) // Checks for leftmost, rightmost columns and dead center
                        {
                            if (Board[i, j])
                            {
                                num++;
                            }
                        }
                    }
                }
            }
            return num;
        }
    }
}
