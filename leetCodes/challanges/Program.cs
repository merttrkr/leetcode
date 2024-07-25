using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MaxPathSum
{
    public class Program
    {
        private const string exampleInput1 =
@"215
193 124
117 237 442
218 935 347 235
320 804 522 417 345
229 601 723 835 133 124
248 202 277 433 207 263 257
359 464 504 528 516 716 871 182
461 441 426 656 863 560 380 171 923
381 348 573 533 447 632 387 176 975 449
223 711 445 645 245 543 931 532 937 541 444
330 131 333 928 377 733 017 778 839 168 197 197
131 171 522 137 217 224 291 413 528 520 227 229 928
223 626 034 683 839 053 627 310 713 999 629 817 410 121
924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

        private const string exampleInput2 =
@"1
8 4
2 6 9
8 5 9 3";

        public static void Main(string[] args)
        {
            // Test with exampleInput2
            TestExampleInput2();

            // Test with exampleInput1
            TestExampleInput1();

            // User file input handling
            Console.WriteLine("\nEnter the file path: (Ex: " + @"D:\input.txt" + ")");
            string filePath = Console.ReadLine();
            string fileContent = File.ReadAllText(filePath);

            var inputRows = SplitStringIntoRows(fileContent);
            var triangleArray = ConvertTo2DArray(inputRows);
            Console.WriteLine("The result from given file input: {0}", FindMaxSum(triangleArray));
        }

        // Test method for exampleInput2
        public static void TestExampleInput2()
        {
            var rows = SplitStringIntoRows(exampleInput2);
            var triangleArray = ConvertTo2DArray(rows);
            var maxSum = FindMaxSum(triangleArray);
            Console.WriteLine("Example 2: Maximum total according to given conditions: {0}", maxSum);
        }

        // Test method for exampleInput1
        public static void TestExampleInput1()
        {
            var rows = SplitStringIntoRows(exampleInput1);
            var triangleArray = ConvertTo2DArray(rows);
            var maxSum = FindMaxSum(triangleArray);
            Console.WriteLine("Example 1: Maximum total according to given conditions: {0}", maxSum);
        }

        // Finds the maximum sum according to the given conditions
        public static int FindMaxSum(int[][] triangle)
        {
            int numRows = triangle.Length;

            // Traverse the triangle from the second last row to the top
            for (int row = numRows - 2; row >= 0; row--)
            {
                for (int col = 0; col < triangle[row].Length; col++)
                {
                    if (triangle[row][col] != 0) // Only consider non-prime numbers
                    {
                        int leftChild = triangle[row + 1][col];
                        int rightChild = triangle[row + 1][col + 1];
                        triangle[row][col] += Math.Max(leftChild, rightChild);
                    }
                }
            }
            return triangle[0][0];
        }

        // Converts a string array into a 2D integer array and replaces primes with zero
        public static int[][] ConvertTo2DArray(string[] rows)
        {
            var triangle = new int[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                triangle[i] = ConvertRowToIntArray(rows[i]);
                for (int j = 0; j < triangle[i].Length; j++)
                {
                    if (IsPrime(triangle[i][j]))
                    {
                        triangle[i][j] = 0; // Replace primes with 0
                    }
                }
            }
            return triangle;
        }

        // Splits the input into rows
        public static string[] SplitStringIntoRows(string input)
        {
            return input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Converts a string row into an integer array
        public static int[] ConvertRowToIntArray(string row)
        {
            return Regex.Matches(row, @"\d+")
                        .Cast<Match>()
                        .Select(m => int.Parse(m.Value))
                        .ToArray();
        }

        // Checks if a number is prime
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;
            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0) return false;
            }
            return true;
        }
    }
}
