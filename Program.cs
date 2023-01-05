using System;

namespace Console2048
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeScreen();

            AnimationStarting();

            while (true) 
            {
                // Initialize the game board
                int[,] board = new int[4, 4];
                
                // Add the first two tiles
                AddRandomTile(board);
                AddRandomTile(board);
                
                // Play the game
                PlayGame(board);
            }
        }

        static void PlayGame(int[,] board) 
        {
            // Game loop
            while (true)
            {
                // Print the game board
                Console.Clear();
                PrintBoard(board);

                // Get the player's input
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        // Move the tiles up
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
                        // Move the tiles down
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
                        // Move the tiles left
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
                    default:
                        break;
                        return;
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
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(4);
                col = random.Next(4);
            } while (board[row, col] != 0);

            // Add a 2 or 4 to the empty cell
            int value = random.Next(2) == 0 ? 2 : 4;
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
                        Console.Write($"{cell,5} ║");
                    }
                }
                Console.WriteLine();

                // Check if the row is the last or not 
                if (row == 3)
                {
                    // Print the bottom border
                    Console.WriteLine("╚══════╩══════╩══════╩══════╝");
                } else 
                {
                    // Print the row separator
                    Console.WriteLine("╟══════╬══════╬══════╬══════╢");
                }
            }
        }

        static void Clear()
        {
            Console.Clear();
        }
    
        static void HomeScreen()
        {
            // ####################################
            // ############ Home screen ###########
            // ####################################
            Clear();
            Console.WriteLine("██████╗░░█████╗░░░██╗██╗░█████╗░  ████████╗██╗░░██╗███████╗  ░██████╗░░█████╗░███╗░░░███╗███████╗");
            Console.WriteLine("╚════██╗██╔══██╗░██╔╝██║██╔══██╗  ╚══██╔══╝██║░░██║██╔════╝  ██╔════╝░██╔══██╗████╗░████║██╔════╝");
            Console.WriteLine("░░███╔═╝██║░░██║██╔╝░██║╚█████╔╝  ░░░██║░░░███████║█████╗░░  ██║░░██╗░███████║██╔████╔██║█████╗░░");
            Console.WriteLine("██╔══╝░░██║░░██║███████║██╔══██╗  ░░░██║░░░██╔══██║██╔══╝░░  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░");
            Console.WriteLine("███████╗╚█████╔╝╚════██║╚█████╔╝  ░░░██║░░░██║░░██║███████╗  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗");
            Console.WriteLine("╚══════╝░╚════╝░░░░░░╚═╝░╚════╝░  ░░░╚═╝░░░╚═╝░░╚═╝╚══════╝  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝");
            Console.WriteLine("\nUse the arrow keys to move the tiles.");
            Console.WriteLine("\nWhen two tiles with the same number touch, they merge into one!");
            Console.WriteLine("\nPress any key to start the game.");
            Console.ReadKey();
            Clear();
        }
    
        static void AnimationStarting()
        {
            // ####################################
            // ##### Animation before starting ####
            // ####################################
            Console.WriteLine("\nThe game will start in...");
            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000); 
            }
            Console.WriteLine("Go!");
            Clear();
        }
    }
}
