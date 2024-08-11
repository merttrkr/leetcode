//Console.WriteLine(Search(new int[] { -1, 0, 3, 5, 9, 12 }, 9));
using System.Reflection;

int Search(int[] nums, int target)
{
    int right = nums.Length - 1;
    int left = 0;
    if (nums[right] < target || nums[left] > target)
    {
        return -1;
    }
    while (right >= left)
    {
        int middle = (right + left) / 2;

        if (nums[middle] == target)
        {
            return middle;
        }
        else if (nums[middle] < target)
        {
            left = middle + 1;
        }
        else
        {
            right = middle - 1;
        }
    }

    //sorted in ascending -1,0,3,5,9,12
    // find middle if it is greater than target 

    return -1;
}
//int[][] matrix = {
//            new int[] {1, 3, 5, 7},
//            new int[] {10, 11, 16, 20},
//            new int[] {23, 30, 34, 60}
//        };
//SearchMatrix( matrix,3);
bool SearchMatrix(int[][] matrix, int target)
{
    int rows = matrix.Length;
    int cols = matrix[0].Length;

    int left = 0;
    int right = (rows * cols) - 1;

    while (left <= right)
    {
        int middle = (left + right) / 2;
        int row = middle / cols;   // Corrected: Use 'cols' instead of 'rows'
        int col = middle % cols;

        if (matrix[row][col] == target)
        {
            return true;
        }
        else if (matrix[row][col] < target)
        {
            left = middle + 1;
        }
        else
        {
            right = middle - 1;
        }
    }
    return false;
}
