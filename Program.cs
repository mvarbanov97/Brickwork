using System;
using System.Collections.Generic;

namespace Brickwork
{
    class Program
    {
        //Area width
        static int width;
        //Area height
        static int height;
        //Brick layout from user input
        static int[][] InputBrickLayer;

        static int maxInput = 100;

        static void Main(string[] args)
        {
            // Do-While so the console ask for the inputs, after invalid inputs are given, instead of closing the program
            bool succes;
            do
            {
                succes = GetInput();
            } while (!succes);


            int[][] secondBrickLayer = new int[height][];
            for (int i = 0; i < height; i++)
            {
                //Set second layer row width
                secondBrickLayer[i] = new int[width];
            }

            var reuslt = FindSecondLayer(InputBrickLayer, secondBrickLayer, 0, 0, 1);


            //Print result
            // TODO: Add symbol separator between bricks */=
            foreach (var row in reuslt)
            {
                foreach (var element in row)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        // FindSecondLayer function finds possible layout of the second layer or returns null if impossible to do so.
        // It traverses through the first layer, represented by a multidimensional array, starting from the top-left cell.
        // InputBrickLayer is base input layer, represented by multidimensional array
        // SecondBrickLayer is layer with the same size as layer one, initially filled with 0 in every single cell.
        public static int[][] FindSecondLayer(int[][] firstLayer, int[][] secondLayer, int row = 0, int col = 0, int brickNumber = 1)
        {
            if (row < secondLayer.Length)
            {
                if (col < secondLayer[0].Length)
                {
                    if (secondLayer[row][col] == 0)
                    {
                        // The function checks if a brick can lie in horizontal or vertical direction. In case it is allowed, it
                        // proceeds with laying the new brick at layer two(writing the respective brick number at the current
                        // cell and the neighbouring cell allowed). Recursion is used in order to check all scenarios for a
                        // possible solution.
                        if (IsHorizontalBrickAllowed(firstLayer, secondLayer, row, col))
                        {
                            secondLayer[row][col] = brickNumber;
                            secondLayer[row][col + 1] = brickNumber;
                            secondLayer = FindSecondLayer(firstLayer, secondLayer, row, col + 2, brickNumber + 1);
                            if (IsSecondLayerComplete(secondLayer))
                            {
                                return secondLayer;
                            }
                        }
                        if (IsVerticalBrickAllowed(firstLayer, secondLayer, row, col))
                        {
                            secondLayer[row][col] = brickNumber;
                            secondLayer[row + 1][col] = brickNumber;
                            secondLayer = FindSecondLayer(firstLayer, secondLayer, row, col + 1, brickNumber + 1);
                            if (IsSecondLayerComplete(secondLayer))
                            {
                                return secondLayer;
                            }
                        }
                        // No solution found
                        return null;
                    }
                    else
                    {
                        secondLayer = FindSecondLayer(firstLayer, secondLayer, row, col + 1, brickNumber);
                        if (IsSecondLayerComplete(secondLayer))
                        {
                            return secondLayer;
                        }
                    }
                }
                else
                {
                    secondLayer = FindSecondLayer(firstLayer, secondLayer, row + 1, 0, brickNumber);
                    if (IsSecondLayerComplete(secondLayer))
                    {
                        return secondLayer;
                    }
                }
            }
            else
            {
                if (IsSecondLayerComplete(secondLayer))
                {
                    return secondLayer;
                }
                // No solution found
                return null;
            }

            return null;
        }

        public static bool IsSecondLayerComplete(int[][] secondLayer)
        {
            for (int i = 0; i < secondLayer.Length; i++)
            {
                for (int j = 0; j < secondLayer[0].Length; j++)
                {
                    if (secondLayer[i][j] == 0)
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public static bool IsHorizontalBrickAllowed(int[][] firstLayer, int[][] secondLayer, int row, int col)
        {
            if (col + 1 < firstLayer[0].Length)
            {
                if (firstLayer[row][col] != firstLayer[row][col + 1] && secondLayer[row][col + 1] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsVerticalBrickAllowed(int[][] firstLayer, int[][] secondLayer, int row, int col)
        {
            if (row + 1 < firstLayer.Length)
            {
                if (firstLayer[row][col] != firstLayer[row + 1][col] && secondLayer[row + 1][col] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool GetInput()
        {
            try
            {
                //Get area dimensions
                Console.WriteLine("Enter the height and width of the area:");

                //Read line, and split it by whitespace into an array of strings
                string[] InputHolder = Console.ReadLine().Split();

                if (InputHolder.Length != 2)
                {
                    Console.WriteLine("Enter two numbers!");
                    return false;
                }

                height = int.Parse(InputHolder[0]);
                width = int.Parse(InputHolder[1]);

                //Check for even numbers and length
                if (height > maxInput || width > maxInput || height % 2 != 0 || width % 2 != 0 || height == 0 || width == 0)
                {
                    Console.WriteLine("Size must be under 100 and an even number!");
                    return false;
                }

                Console.WriteLine("Enter the brick layout:");

                //Set layout rows
                InputBrickLayer = new int[height][];

                for (int i = 0; i < height; i++)
                {
                    //Set row width
                    InputBrickLayer[i] = new int[width];

                    InputHolder = Console.ReadLine().Split();

                    //Check for right number count
                    if (InputHolder.Length != width)
                    {
                        Console.WriteLine("Incorect number count!");
                        return false;
                    }

                    //Fill row with elements
                    for (int j = 0; j < width; j++)
                    {
                        int brickNumber = int.Parse(InputHolder[j]);
                        InputBrickLayer[i][j] = brickNumber;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input!");
                return false;
            }

            return true;
        }
    }
}
