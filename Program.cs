using System;
using System.Drawing;

namespace Console2048
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to 2048!");
            Console.WriteLine("Use the arrow keys to move the tiles.");
            Console.WriteLine("When two tiles with the same number touch, they merge into one!");
            Console.WriteLine("Press any key to start the game.");
            Console.ReadKey();

            // Initialize the game board
            int[,] board = new int[4, 4];
            // Add the first two tiles
            AddRandomTile(board);
            AddRandomTile(board);

            // Initialize the score
            int score = 0;
            int bestScore = 0; 

            // Game loop
            while (true)
            {
                // Print the game board
                Console.Clear();
                PrintBoard(board);

                // Display the score and the best score
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Best score: " + bestScore);

                // Get the player's input
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        for (int col = 0; col < 4; col++)
                            {
                                int[] column = new int[4];
                                for (int row = 0; row < 4; row++)
                                {
                                    column[row] = board[row, col];
                                }
                                int[] merged = Merge(column);
                                for (int row = 0; row < 4; row++)
                                {
                                    board[row, col] = merged[row];
                                }
                            }
                        break;
                    case ConsoleKey.DownArrow:
                        for (int col = 3; col >= 0; col--)
                        {
                            int[] column = new int[4];
                            for (int row = 3; row >= 0; row--)
                            {
                                column[3 - row] = board[row, col];
                            }
                            int[] merged = Merge(column);
                            for (int row = 3; row >= 0; row--)
                            {
                                board[row, col] = merged[3 - row];
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        for (int row = 0; row < 4; row++)
                        {
                            int[] line = new int[4];
                            for (int col = 0; col < 4; col++)
                            {
                                line[col] = board[row, col];
                            }
                            int[] merged = Merge(line);
                            for (int col = 0; col < 4; col++)
                            {
                                board[row, col] = merged[col];
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        // Move the tiles right
                        for (int row = 3; row >= 0; row--)
                        {
                            int[] line = new int[4];
                            for (int col = 3; col >= 0; col--)
                            {
                                line[3 - col] = board[row, col];
                            }
                            int[] merged = Merge(line);
                            for (int col = 3; col >= 0; col--)
                            {
                                board[row, col] = merged[3 - col];
                            }
                        }
                        break;
                }
                
                // Add a new random tile to the board
                AddRandomTile(board);
            }
        }

        static int[] Merge(int[] column)
        {
            // Move the tiles towards the front of the array
            int[] result = new int[4];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                if (column[i] != 0)
                {
                    result[index] = column[i];
                    index++;
                }
            }

            // Merge the tiles
            for (int i = 0; i < 3; i++)
            {
                if (result[i] == result[i + 1])
                {
                    result[i] *= 2;
                    result[i + 1] = 0;
                }
            }

            // Move the tiles towards the front of the array again
            index = 0;
            for (int i = 0; i < 4; i++)
            {
                if (result[i] != 0)
                {
                    result[index] = result[i];
                    index++;
                }
            }
            for (int i = index; i < 4; i++)
            {
                result[i] = 0;
            }

            return result;
        }

        static void AddRandomTile(int[,] board)
        {
            // Find a random empty cell
            Random rnd = new Random();
            int row, col;
            do
            {
                row = rnd.Next(4);
                col = rnd.Next(4);
            } while (board[row, col] != 0);

            // Add a 2 or 4 to the empty cell
            int value = rnd.Next(2) == 0 ? 2 : 4;
            board[row, col] = value;
        }

        static void PrintBoard(int[,] board)
        {
            // Print the top border
            Console.WriteLine("╔══════╦══════╦══════╦══════╗");

            // Print the rows
            for (int row = 0; row < 4; row++)
            {
                // Print the left border
                Console.Write("║");

                // Print the cells
                for (int col = 0; col < 4; col++)
                {
                    int cell = board[row, col];
                    if (cell == 0)
                    {
                        Console.Write("      ║");
                    }
                    else
                    {
                        Color color = GetColor(cell);
                        Console.ForegroundColor = (consoleColor)Enum.Parse(typeof(consoleColor), color.Name);
                        Console.Write($"{cell,5} ║");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();

                if (row == 3)
                {
                    Console.WriteLine("╚══════╩══════╩══════╩══════╝");
                } else 
                {
                    // Print the row separator
                    Console.WriteLine("╟══════╬══════╬══════╬══════╢");
                }
            }
        }

        static void DisplayBoard(int[,] board)
        {
            Console.Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Console.Write(board[row, col]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static int GetPoints(int[,] board)
        {
            int points = 0;
            bool merged = false;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] > 2)
                    {
                        merged = true;
                        points += board[row, col] * 2;
                    }
                }
            }
            return merged ? points : 0;
        }

        static Color GetColor(int cell)
        {
            switch (cell)
            {
                case 2: return Color.FromArgb(238, 228, 218);
                case 4: return Color.FromArgb(237, 224, 200);
                case 8: return Color.FromArgb(242, 177, 121);
                case 16: return Color.FromArgb(245, 149, 99);
                case 32: return Color.FromArgb(246, 124, 95);
                case 64: return Color.FromArgb(246, 94, 59);
                case 128: return Color.FromArgb(237, 207, 114);
                case 256: return Color.FromArgb(237, 204, 97);
                case 512: return Color.FromArgb(237, 200, 80);
                case 1024: return Color.FromArgb(237, 197, 63);
                case 2048: return Color.FromArgb(237, 194, 46);
                default: return Color.FromArgb(205, 193, 180);
            }
        }

        static bool HasLost(int[,] board)
        {
            // Check if there are any empty cells
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == 0)
                    {
                        return false;
                    }
                }
            }

            // Check if any adjacent cells have the same value
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (row > 0 && board[row, col] == board[row - 1, col])
                    {
                        return false;
                    }
                    if (row < 3 && board[row, col] == board[row + 1, col])
                    {
                        return false;
                    }
                    if (col > 0 && board[row, col] == board[row, col - 1])
                    {
                        return false;
                    }
                    if (col < 3 && board[row, col] == board[row, col + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        static bool HasWon(int[,] board)
        {
            // Check if there is a 2048 tile
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
