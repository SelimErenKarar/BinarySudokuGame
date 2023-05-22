using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a 2D array for the table
            int[,] table = new int[9, 9];

            // Assigning every element an empty value (Value 2 means that point is empty.)
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = 2;
                }
            }

            // Displaying the game screen
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.SetCursorPosition(50, 1);
            Console.WriteLine("New Piece");

            Console.SetCursorPosition(80, 1);
            Console.Write("Score : ");

            Console.SetCursorPosition(80, 3);
            Console.WriteLine("Piece : ");

            Console.SetCursorPosition(2, 21);
            Console.Write("(Press ESC to exit)");

            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i % 3 == 0 && j % 3 == 0)
                    {
                        // + signs
                        Console.SetCursorPosition(i * 4 + 2, j * 2 + 1);
                        Console.WriteLine("+");

                        // - signs
                        if (i != 9)
                        {
                            Console.SetCursorPosition(i * 4 + 3, j * 2 + 1);
                            Console.WriteLine("-----------");
                        }

                        // | symbols
                        if (j != 9)
                        {
                            for (int k = 2; k < 7; k++)
                            {
                                Console.SetCursorPosition(i * 4 + 2, j * 2 + k);
                                Console.WriteLine("|");
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    // Numbers of columns
                    Console.SetCursorPosition(i * 4 + 4, 0);
                    Console.WriteLine(i + 1);

                    // Numbers of rows
                    Console.SetCursorPosition(0, i * 2 + 2);
                    Console.WriteLine(i + 1);
                }
            }

            Console.ResetColor();

            // Creating a 3D array with all the possible shape types
            // (Value 2 means that point is empty, value 3 means that point has 0 or 1)
            int[,,] shapes = { { { 3, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } }, { { 3, 3, 2 }, { 2, 2, 2 }, { 2, 2, 2 } }, { { 3, 2, 2 }, { 3, 2, 2 }, { 2, 2, 2 } },
                { { 3, 3, 3 }, { 2, 2, 2 }, { 2, 2, 2 } },{ { 3, 2, 2 }, { 3, 2, 2 }, { 3, 2, 2 } },{ { 3, 3, 2 }, { 3, 2, 2 }, { 2, 2, 2 } },
                { { 3, 3, 2 }, { 2, 3, 2 }, { 2, 2, 2 } },{ { 3, 2, 2 }, { 3, 3, 2 }, { 2, 2, 2 } }, { { 2, 3, 2 }, { 3, 3, 2 }, { 2, 2, 2 } },{ { 3, 3, 2 }, { 3, 3, 2 }, { 2, 2, 2 } }};

            // Creating a 2D array with all the possible shape sizes
            int[,] shape_size = { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 0, 2 }, { 2, 0 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 } };

            // Creating a random variable for selecting a shape
            Random rnd_shape = new Random();

            // Creating a random variable for selecting a value for each element of the shape
            Random rnd_binary = new Random();

            // Choosing a shape number between 0 and 10 randomly
            int shape_no = rnd_shape.Next(shapes.GetLength(0));

            // Creating a 2D array for the shape
            int[,] shape = new int[3, 3];

            // (Using nested loops to) Assigning each element of the shape a random value (unless it's empty)
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    shape[i, j] = shapes[shape_no, i, j];
                    if (shape[i, j] == 3)
                    {
                        shape[i, j] = rnd_binary.Next(0, 2);
                    }
                }
            }


            ConsoleKeyInfo cki;

            // Index variables for table elements
            int tablex, tabley;

            // Cursor coordinates
            int cursorx = 52, cursory = 4;

            int total_score = 0;

            int shape_counter = 1;

            bool game_over = false;

            // General game loop, will continue untill the game is over  
            while (!game_over)
            {
                // Each tour, displaying the total score and the number of shapes generated
                Console.SetCursorPosition(88, 1);
                Console.WriteLine(total_score);

                Console.SetCursorPosition(88, 3);
                Console.WriteLine(shape_counter);

                // Making cursor disappear
                Console.CursorVisible = false;


                if (Console.KeyAvailable)
                {
                    // Keeps the key player pressed
                    cki = Console.ReadKey(true);

                    // If the right arrow key pressed, moving the shape to right
                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 50 - shape_size[shape_no, 0])
                    {
                        // Deleting the shape from it's current location
                        for (int i = 0; i < shape.GetLength(0); i++)
                        {
                            Console.SetCursorPosition(cursorx + i * 4, cursory);
                            for (int j = 0; j < shape.GetLength(1); j++)
                            {
                                Console.SetCursorPosition(cursorx + i * 4, cursory + j * 2);
                                tablex = (cursorx + i * 4 - 4) / 4;
                                tabley = (cursory + j * 2 - 2) / 2;

                                // If the shape is inside of the table, placing a dot in place of it
                                if (tablex < 9 && tabley < 9)
                                {
                                    if (table[tablex, tabley] == 2)
                                    {
                                        Console.Write(".");
                                    }
                                    else
                                    {
                                        Console.Write(table[tablex, tabley]);
                                    }
                                }
                                // If the shape isn't inside of the table, placing an empty space in place of it
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                        }

                        // Moving the shape to right
                        cursorx += 4;
                    }

                    // If the left arrow key pressed, moving the shape to left
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4)
                    {
                        for (int i = 0; i < shape.GetLength(0); i++)
                        {
                            Console.SetCursorPosition(cursorx + i * 4, cursory);
                            for (int j = 0; j < shape.GetLength(1); j++)
                            {
                                Console.SetCursorPosition(cursorx + i * 4, cursory + j * 2);
                                tablex = (cursorx + i * 4 - 4) / 4;
                                tabley = (cursory + j * 2 - 2) / 2;

                                if (tablex < 9 && tabley < 9)
                                {
                                    if (table[tablex, tabley] == 2)
                                    {
                                        Console.Write(".");
                                    }
                                    else
                                    {
                                        Console.Write(table[tablex, tabley]);
                                    }
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        cursorx -= 4;
                    }

                    // If the up arrow key pressed, moving the shape to up
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 3)
                    {
                        for (int i = 0; i < shape.GetLength(0); i++)
                        {
                            Console.SetCursorPosition(cursorx + i * 4, cursory);
                            for (int j = 0; j < shape.GetLength(1); j++)
                            {
                                Console.SetCursorPosition(cursorx + i * 4, cursory + j * 2);
                                tablex = (cursorx + i * 4 - 4) / 4;
                                tabley = (cursory + j * 2 - 2) / 2;

                                if (tablex < 9 && tabley < 9)
                                {
                                    if (table[tablex, tabley] == 2)
                                    {
                                        Console.Write(".");
                                    }
                                    else
                                    {
                                        Console.Write(table[tablex, tabley]);
                                    }
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        cursory -= 2;
                    }

                    // If the down arrow key pressed, moving the shape to down
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 18 - shape_size[shape_no, 1] * 2)
                    {
                        for (int i = 0; i < shape.GetLength(0); i++)
                        {
                            Console.SetCursorPosition(cursorx + i * 4, cursory);
                            for (int j = 0; j < shape.GetLength(1); j++)
                            {
                                Console.SetCursorPosition(cursorx + i * 4, cursory + j * 2);
                                tablex = (cursorx + i * 4 - 4) / 4;
                                tabley = (cursory + j * 2 - 2) / 2;

                                if (tablex < 9 && tabley < 9)
                                {
                                    if (table[tablex, tabley] == 2)
                                    {
                                        Console.Write(".");
                                    }
                                    else
                                    {
                                        Console.Write(table[tablex, tabley]);
                                    }
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                        }
                        cursory += 2;
                    }

                    // If the enter key pressed, checking if the shape is inside of the table 
                    if (cursorx <= 40 - (shape_size[shape_no, 0] + 1) * 4 && cursorx > 3 && cursory < 19 && cursory > 1)
                    {
                        if (cki.Key == ConsoleKey.Enter)
                        {
                            // To check if the area is empty where the shape is going to be placed
                            bool empty = true;

                            // Index variables for table elements
                            int a, b;

                            // For each element of the shape, checking if the table's that point is empty or not
                            for (int k = 0; k < shape_size[shape_no, 0] + 1; k++)
                            {
                                for (int m = 0; m < shape_size[shape_no, 1] + 1; m++)
                                {
                                    a = (cursorx + k * 4 - 4) / 4;
                                    b = (cursory + m * 2 - 2) / 2;

                                    if (table[a, b] != 2 && shape[k, m] != 2)
                                    {
                                        empty = false;
                                    }
                                }
                            }

                            // If there is enough space for the shape, placing the shape on the table
                            if (empty == true)
                            {
                                for (int k = 0; k < shape_size[shape_no, 0] + 1; k++)
                                {
                                    for (int m = 0; m < shape_size[shape_no, 1] + 1; m++)
                                    {
                                        a = (cursorx + k * 4 - 4) / 4;
                                        b = (cursory + m * 2 - 2) / 2;

                                        if (table[a, b] == 2)
                                        {
                                            table[a, b] = shape[k, m];
                                        }
                                    }
                                }
                            }

                            // Each tour, creating a counter for the full rows, columns or shapes
                            int full_counter = 0;

                            // Checking if any of the rows is full
                            // Creating an array to keep which rows are full
                            int[] which_rows = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                            // Creating a 2D array to keep the full rows
                            int[,] full_rows = new int[9, 9];

                            bool row_check;
                            // Checking every row of the table for full rows
                            for (int i = 0; i < table.GetLength(0); i++)
                            {
                                row_check = true;
                                for (int j = 0; j < table.GetLength(1); j++)
                                {
                                    if (table[j, i] == 2)
                                    {
                                        row_check = false;
                                        break;
                                    }
                                }

                                // If a row is full, keeping every element of that row inside the full_rows array
                                if (row_check == true)
                                {
                                    for (int j = 0; j < table.GetLength(1); j++)
                                    {
                                        full_rows[j, i] = table[j, i];
                                    }

                                    // Keeping the row number inside the which_rows array
                                    which_rows[i] = 1;
                                    full_counter++;
                                }
                            }

                            // Checking if any of the columns is full
                            // Creating an array to keep which columns are full
                            int[] which_columns = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                            // Creating a 2D array to keep the full columns
                            int[,] full_columns = new int[9, 9];

                            bool column_check;
                            // Checking every column of the table for full columns
                            for (int i = 0; i < table.GetLength(1); i++)
                            {
                                column_check = true;
                                for (int j = 0; j < table.GetLength(0); j++)
                                {
                                    if (table[i, j] == 2)
                                    {
                                        column_check = false;
                                        break;
                                    }
                                }
                                // If a column is full, keeping every element of that column inside the full_columns array
                                if (column_check == true)
                                {
                                    for (int j = 0; j < table.GetLength(0); j++)
                                    {
                                        full_columns[i, j] = table[i, j];
                                    }

                                    // Keeping the column number inside the which_columns array
                                    which_columns[i] = 1;
                                    full_counter++;
                                }
                            }

                            // Checking if any of the blocks is full
                            // Creating a 2D array to keep which blocks are full
                            int[,] which_blocks = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

                            // Creating a 2D array to keep the full blocks
                            int[,] full_blocks = new int[9, 9];

                            bool block_check;
                            // Checking every block of the table for full blocks
                            for (int m = 0; m < shape.GetLength(0); m++)
                            {
                                for (int k = 0; k < shape.GetLength(1); k++)
                                {
                                    block_check = true;
                                    for (int i = m * 3; i < m * 3 + 3; i++)
                                    {
                                        for (int j = k * 3; j < k * 3 + 3; j++)
                                        {
                                            if (table[j, i] == 2)
                                            {
                                                block_check = false;
                                                break;
                                            }
                                        }
                                        if (!block_check)
                                        {
                                            break;
                                        }
                                    }

                                    // If a block is full, keeping every element of that block inside the full_blocks array
                                    if (block_check == true)
                                    {
                                        for (int i = m * 3; i < m * 3 + 3; i++)
                                        {
                                            for (int j = k * 3; j < k * 3 + 3; j++)
                                            {
                                                full_blocks[j, i] = table[j, i];
                                            }
                                        }

                                        // Keeping the block number inside the which_blocks array
                                        which_blocks[m, k] = 1;
                                        full_counter++;
                                    }
                                }
                            }

                            // Each tour, creating a variable for the score of full rows, columns or shapes
                            int tour_score = 0;

                            // Calculating the score of each full row and deleting full rows from the table
                            for (int i = 0; i < which_rows.Length; i++)
                            {
                                if (which_rows[i] == 1)
                                {
                                    for (int j = which_rows.Length - 1; j >= 0; j--)
                                    {
                                        // Deleting the element of the row
                                        table[j, i] = 2;

                                        // Converting binary array to decimal
                                        // Calculating the power of each element in binary array and adding it to score
                                        int pow = j - 8;
                                        if (pow < 0)
                                        {
                                            pow = -pow;
                                        }

                                        int digit = 1;
                                        for (int k = 1; k <= pow; k++)
                                        {
                                            digit *= 2;
                                        }

                                        if (full_rows[j, i] == 1)
                                        {
                                            tour_score += digit;
                                        }
                                    }
                                }
                            }

                            // Calculating the score of each full column and deleting full columns from the table
                            for (int i = 0; i < which_columns.Length; i++)
                            {
                                if (which_columns[i] == 1)
                                {
                                    for (int j = which_columns.Length - 1; j >= 0; j--)
                                    {
                                        // Deleting the element of the column
                                        table[i, j] = 2;

                                        // Converting binary array to decimal
                                        // Calculating the power of each element in binary array and adding it to score
                                        int pow = j - 8;

                                        if (pow < 0)
                                        {
                                            pow = -pow;
                                        }

                                        int digit = 1;
                                        for (int k = 1; k <= pow; k++)
                                        {
                                            digit *= 2;
                                        }

                                        if (full_columns[i, j] == 1)
                                        {
                                            tour_score += digit;
                                        }
                                    }
                                }
                            }

                            // Calculating the score of each full block and deleting full blocks from the table
                            for (int i = 0; i < which_blocks.GetLength(0); i++)
                            {
                                for (int j = 0; j < which_blocks.GetLength(1); j++)
                                {
                                    if (which_blocks[i, j] == 1)
                                    {
                                        // Keeping the elements of the block in a binary array
                                        int[] block_binary = new int[9];


                                        for (int n = i * 3; n <= i * 3 + 2; n++)
                                        {
                                            for (int m = j * 3; m <= j * 3 + 2; m++)
                                            {
                                                // Deleting the element of the block
                                                table[m, n] = 2;
                                                block_binary[j * 3 + 2 - m + (i * 3 + 2 - n) * 3] = full_blocks[m, n];
                                            }
                                        }

                                        for (int l = 0; l < block_binary.Length; l++)
                                        {
                                            // Converting binary array to decimal
                                            // Calculating the power of each element in binary array and adding it to score
                                            // Since the array is already reversed, using left to right order
                                            int pow = l;

                                            int digit = 1;
                                            for (int k = 1; k <= pow; k++)
                                            {
                                                digit *= 2;
                                            }

                                            if (block_binary[l] == 1)
                                            {
                                                tour_score += digit;
                                            }
                                        }
                                    }
                                }
                            }

                            // Multiplying the tour score with how many full rows occured at the same time
                            tour_score *= full_counter;

                            total_score += tour_score;

                            // If the shape is placed, generating a new shape
                            if (empty == true)
                            {
                                shape_no = rnd_shape.Next(shapes.GetLength(0));

                                shape = new int[3, 3];

                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        shape[i, j] = shapes[shape_no, i, j];

                                        if (shape[i, j] == 3)
                                        {
                                            shape[i, j] = rnd_binary.Next(0, 2);
                                        }
                                    }
                                }

                                // Increasing shape counter by 1
                                shape_counter++;
                            }

                            // After generating a new shape, checking if this shape can be placed anywhere in the table, if not the game must end
                            if (empty == true)
                            {
                                // First, checking if the empty space number in the table is less than element number of the shape
                                int space_counter = 0;
                                for (int i = 0; i < table.GetLength(0); i++)
                                {
                                    for (int j = 0; j < table.GetLength(1); j++)
                                    {
                                        if (table[i, j] == 2)
                                        {
                                            space_counter++;
                                        }
                                    }
                                }

                                if (space_counter < shape_size[shape_no, 0] + shape_size[shape_no, 1] + 1)
                                {
                                    game_over = true;
                                }

                                // If there is enough many empty space, then trying to place the shape in each location
                                if (game_over == false)
                                {
                                    game_over = true;

                                    // To check if there is an available place for shape to be placed
                                    bool available;
                                    for (int i = 2; i <= 20 - (shape_size[shape_no, 1] + 1) * 2; i += 2)
                                    {
                                        for (int j = 4; j <= 40 - (shape_size[shape_no, 0] + 1) * 4; j += 4)
                                        {
                                            available = true;
                                            for (int k = 0; k < shape_size[shape_no, 0] + 1; k++)
                                            {
                                                for (int m = 0; m < shape_size[shape_no, 1] + 1; m++)
                                                {
                                                    a = (j + k * 4 - 4) / 4;
                                                    b = (i + m * 2 - 2) / 2;

                                                    if (table[a, b] != 2 && shape[k, m] != 2)
                                                    {
                                                        available = false;
                                                        break;
                                                    }
                                                }
                                                if (!available)
                                                {
                                                    break;
                                                }
                                            }
                                            if (available)
                                            {
                                                game_over = false;
                                                break;
                                            }
                                        }
                                        if (!game_over)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            // Setting the coordinates back to beginning
                            cursorx = 52;
                            cursory = 4;
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }

                    // If the escape key pressed, the game must end  
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        game_over = true;
                    }
                }

                // Displaying the shape 
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(cursorx, cursory);
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    Console.SetCursorPosition(cursorx + i * 4, cursory);
                    for (int j = 0; j < shape.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(cursorx + i * 4, cursory + j * 2);
                        if (shape[i, j] != 2)
                        {
                            Console.Write(shape[i, j]);
                        }
                    }
                }

                // Displaying the table depending on the shape's location
                Console.ForegroundColor = ConsoleColor.Yellow;

                // To check if any of the shape's element is overlapping the table's element
                bool overlap;

                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        overlap = false;
                        // If the table element is empty and if there is a shape on that location and it's element is also empty, displaying a dot
                        Console.SetCursorPosition(i * 4 + 4, j * 2 + 2);
                        if (table[i, j] == 2)
                        {
                            for (int k = 0; k < shape.GetLength(0); k++)
                            {
                                for (int m = 0; m < shape.GetLength(0); m++)
                                {
                                    if (cursorx + k * 4 == i * 4 + 4 && cursory + m * 2 == j * 2 + 2)
                                    {
                                        overlap = true;
                                        if (shape[k, m] == 2)
                                        {
                                            Console.Write(".");
                                        }
                                    }
                                }
                            }

                            if (overlap == false)
                            {
                                Console.Write(".");
                            }
                        }

                        // If the table's element is not empty and if there is a shape on that location and it's element is also empty, displaying the element of the table 
                        else
                        {
                            for (int k = 0; k < shape.GetLength(0); k++)
                            {
                                for (int m = 0; m < shape.GetLength(0); m++)
                                {
                                    if (cursorx + k * 4 == i * 4 + 4 && cursory + m * 2 == j * 2 + 2)
                                    {
                                        overlap = true;
                                        if (shape[k, m] == 2)
                                        {
                                            Console.Write(table[i, j]);
                                        }
                                    }
                                }
                            }

                            if (overlap == false)
                            {
                                Console.Write(table[i, j]);
                            }
                        }
                    }
                }
            }

            // If the game is over, clearing the screen, displaying the total score, total piece number, game over text and last version of the table 
            Console.Clear();

            Console.SetCursorPosition(47, 11);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Score: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(total_score);

            Console.SetCursorPosition(47, 13);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Total piece: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(shape_counter);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("   ####      ######   ###        ### ########      ######   ##            ## ########  #######");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("  ######    ########  ## #      # ## ########     ########   ##          ##  ########  ##    ##");
            Console.SetCursorPosition(10, 4);
            Console.WriteLine(" ##        ##      ## ##  #    #  ## ##           ##    ##    ##        ##   ##        ##     ##");
            Console.SetCursorPosition(10, 5);
            Console.WriteLine(" ##   ###  ##      ## ##   #  #   ## ######       ##    ##     ##      ##    #######   ##    ##");
            Console.SetCursorPosition(10, 6);
            Console.WriteLine(" ##  ##### ########## ##    ##    ## ######       ##    ##      ##    ##     #######   ##  ####");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine(" ##     ## ########## ##    ##    ## ##           ##    ##       ##  ##      ##        ##    ##");
            Console.SetCursorPosition(10, 8);
            Console.WriteLine("  #######  ##      ## ##          ## ########     ########        # #        ########  ##     ##");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("   #####   ##      ## ##          ## ########      ######          #         ########  ##      ##");

            // Displaying the table
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i % 3 == 0 && j % 3 == 0)
                    {
                        // + signs
                        Console.SetCursorPosition((i * 4 + 2) + 35, (j * 2 + 1) + 15);
                        Console.WriteLine("+");

                        // - signs
                        if (i != 9)
                        {
                            Console.SetCursorPosition((i * 4 + 3) + 35, (j * 2 + 1) + 15);
                            Console.WriteLine("-----------");
                        }

                        // | symbols
                        if (j != 9)
                        {
                            for (int k = 2; k < 7; k++)
                            {
                                Console.SetCursorPosition((i * 4 + 2) + 35, (j * 2 + k) + 15);
                                Console.WriteLine("|");
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    // Number of rows
                    Console.SetCursorPosition((i * 4 + 4) + 35, 15);
                    Console.WriteLine(i + 1);

                    // Number of columns
                    Console.SetCursorPosition(35, (i * 2 + 2) + 15);
                    Console.WriteLine(i + 1);
                }
            }

            // Values in the table
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {

                    // If the element is empty, displaying a dot
                    Console.SetCursorPosition((i * 4 + 4) + 35, (j * 2 + 2) + 15);
                    if (table[i, j] == 2)
                    {
                        Console.Write(".");
                    }

                    // If the element is not empty, displaying the value of the element
                    else
                    {
                        Console.Write(table[i, j]);
                    }
                }
            }


            Console.ReadLine();
        }
    }
}