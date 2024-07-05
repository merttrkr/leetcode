using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArraysAndHashing;

public class Solution
{
    public static bool ContainsDuplicate(int[] nums)
    {
        Dictionary<int,int> dict = new();
        foreach (int num in nums)
        {
            if (dict.ContainsKey(num))
            {
                return true;
            }
            dict.Add(num, 1);
        }
        return false;
    }
    public static bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        Dictionary<char, int> charCount = new();

        // Count characters in the first string
        foreach (char c in s)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Subtract characters count based on the second string
        foreach (char c in t)
        {
            if (charCount.ContainsKey(c))
            {
                charCount[c]--;
                if (charCount[c] == 0)
                    charCount.Remove(c);
            }
            else
            {
                return false;
            }
        }

        // If the dictionary is empty, the strings are anagrams
        return charCount.Count == 0;
    }
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var map = new Dictionary<string, IList<string>>();

        foreach (var word in strs)
        {
            var charCount = new int[26]; // Assuming only lowercase alphabets (a-z)

            foreach (var ch in word)
            {
                Console.WriteLine("ch in word " + ch );
                charCount[ch - 'a']++;
            }

            var key = string.Join(",", charCount);

            if (!map.ContainsKey(key))
            {
                map.Add(key, new List<string>());
            }

            map[key].Add(word);
        }

        return map.Values.ToList();
    }
    public static int[] TwoSum(int[] numbers, int target)
    {
        HashSet<int> hs = new ();

        for (int i = 0; i < numbers.Length; i++)
        {
            int searchResult = target - numbers[i];
            if (hs.Contains(searchResult))
            {
                return new int[] {Array.IndexOf(numbers,searchResult)+1,i+1};
            }
            hs.Add(numbers[i]);
        }
        return null;
    }

    public static int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int,int> result = nums.GroupBy(x => x).ToDictionary(num => num.Key, num => num.Count());
        return result.OrderByDescending(x => x.Value).Take(k).Select(x => x.Key).ToArray();
        
    }
    public static string Encode(IList<string> strs)
    {
        return string.Join("", strs.Select(str => str + str.Length));
    }

    public static List<string> Decode(string s)
    {
        List<string> result = new List<string>();
        StringBuilder sb = new StringBuilder();
        foreach (char c in s)
        {
            if (Char.IsDigit(c))
            {
                result.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }
        return result;
    }

    public static int[] ProductExceptSelf(int[] nums)
    {

        int length = nums.Length;
        int[] result = new int[length];
        int temp = 1;

        for (int i = 0; i < length; i++)
        {
            result[i] = temp;
            temp *= nums[i];
        }

        temp = 1;
        for (int j = length - 1; j >= 0; j--)
        {
            result[j] *= temp;
            temp *= nums[j];
        }
        return result;
    }
    public static bool IsValidSudoku(char[][] board)
    {
        int square=(int)Math.Sqrt(board.Length);

        HashSet <string> row = new ();

        for (int i = 0; i < board.Length;i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                char number = board[i][j];
                if (char.IsDigit(number))
                {
                    if (!row.Add(number + "r"+i) || 
                        !row.Add(number +"c"+j)  || 
                        !row.Add(number +" b "+ i / square + "-" + j / square))
                    {
                        return false;
                    }

                }
            }
        }
        return true;
    }
    public static int LongestConsecutive(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        int[] sortedNums = nums.OrderBy(x => x).ToArray();
        Dictionary<int, int> valueAndConsequence= new();

        for(int i = 0; i < sortedNums.Length; i++)
        {
            int previousValue = 0;

            if (valueAndConsequence.TryGetValue(sortedNums[i] - 1, out previousValue))
            {
                
                valueAndConsequence.TryAdd(sortedNums[i], previousValue+1);
            }
            else
            {
                valueAndConsequence.TryAdd(sortedNums[i], 1);
            }
        }


        return valueAndConsequence.Values.Max();
    }
    public static int LongestConsecutiveHashset(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        HashSet<int> numSet = new HashSet<int>(nums);
        int longestStreak = 0;

        foreach (int num in numSet)
        {
            // Only start a sequence if `num-1` is not in the set
            if (!numSet.Contains(num - 1))
            {
                int currentNum = num;
                int currentStreak = 1;

                while (numSet.Contains(currentNum + 1))
                {
                    currentNum++;
                    currentStreak++;
                }

                longestStreak = Math.Max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
    public static bool IsPalindrome(string s)
    {
        string pattern = @"[^a-zA-Z0-9]";
        s= Regex.Replace(s, pattern, "").ToLower();

        if (string.IsNullOrEmpty(s)) return true;

        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            if (s[left] != s[right])
            {
                return false;
            }
            left++;
            right--;
        }

        return true;
    }
    public static bool IsPalindromeLinq(string s)
    {
        string pattern = @"[^a-zA-Z0-9]";
        s = Regex.Replace(s, pattern, "").ToLower();

        if (string.IsNullOrEmpty(s))
        {
            return true; // Empty string is considered a palindrome
        }
        return s.SequenceEqual(s.Reverse());

    }
    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();
        if (nums.Length < 3)
        {
            return result;
        }

        Array.Sort(nums);
        for (int left = 0; left < nums.Length - 2; left++)
        {
            if (left > 0 && nums[left] == nums[left - 1])
            {
                continue; // Skip duplicates for the left pointer
            }

            int leftPlus = left + 1;
            int right = nums.Length - 1;

            while (leftPlus < right)
            {
                int sum = nums[left] + nums[leftPlus] + nums[right];
                if (sum == 0)
                {
                    result.Add(new List<int> { nums[left], nums[leftPlus], nums[right] });

                    // Skip duplicates for the leftPlus pointer
                    while (leftPlus < right && nums[leftPlus] == nums[leftPlus + 1])
                    {
                        leftPlus++;
                    }

                    // Skip duplicates for the right pointer
                    while (leftPlus < right && nums[right] == nums[right - 1])
                    {
                        right--;
                    }

                    leftPlus++;
                    right--;
                }
                else if (sum < 0)
                {
                    leftPlus++;
                }
                else
                {
                    right--;
                }
            }
        }
        return result;
    }
    public static int MaxArea(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int max = 0;
       while(left < right)
        {
            int x = right - left;
            int y = height[left] > height[right] ? height[right] : height[left];
            if (x*y > max)
            {
                max = x * y;
            }
            if (height[left] > height[right])
            {
                right --;
            }
            else
            {
                left ++;
            }
        }
       return max;
    }
    public static int  Trap(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int maxLeft = 0;
        int maxRight = 0;
        int totalWater = 0;

        while (left < right)
        {
            if (height[left] < height[right])
            {
                if (height[left] >= maxLeft)
                {
                    maxLeft = height[left];
                }
                else
                {
                    totalWater += maxLeft - height[left];
                }
                left++;
            }
            else
            {
                if (height[right] >= maxRight)
                {
                    maxRight = height[right];
                }
                else
                {
                    totalWater += maxRight - height[right];
                }
                right--;
            }
        }
        return totalWater;
    }
    public static void Main(string[] args)
    {
        int[] nums = { 1,2,3,4};
        string s = "ana";
        string[] strs = { "eat", "tea", "tan", "ate", "nat", "bat" };

        //Console.WriteLine(ContainsDuplicate(nums));

        //Console.WriteLine(IsAnagram(s, s));

        //GroupAnagrams(strs);

        //int[] arr = TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2);
        //foreach (var item in arr)
        //{
        //    Console.WriteLine(item);
        //}

        //Console.WriteLine(Encode(new List<string> {"test","top","ecemmert" }));

        //List<string> strArr = Decode(Encode(new List<string> { "test", "top", "ecemmert" }));
        //foreach (var item in strArr)
        //{
        //    Console.WriteLine(item);
        //}

        //int[] arr= ProductExceptSelf(nums);
        //foreach (var item in arr)
        //{
        //    Console.WriteLine(item);
        //}
        //char[][] validBoard = new char[][] {
        //new char[] {'9', '3', '.', '.', '7', '.', '.', '.', '.'},
        //new char[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
        //new char[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
        //new char[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
        //new char[] {'4', '.', '.', '8', '.', '.', '.', '1', '.'},
        //new char[] {'7', '.', '.', '.', '2', '.', '.', '.', '5'},
        //new char[] {'.', '.', '.', '.', '.', '.', '.', '.', '9'},
        //new char[] {'.', '.', '.', '.', '.', '.', '4', '.', '.'},
        //new char[] {'.', '.', '.', '.', '.', '.', '.', '.', '1'}
        //};
        //Console.WriteLine(IsValidSudoku(validBoard));

        //int[] arr = {100,4,200,1,3,2,5 };

        //Console.WriteLine(LongestConsecutive(arr)); 

        //Console.WriteLine(IsPalindromeLinq("A man, a plan, a canal: Panama")); 

        //int [][] list = ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 }).Select(innerList => innerList.ToArray()).ToArray();

        //Console.WriteLine(string.Join(" | ", list.Select(arr => string.Join(",", arr))));

        Console.WriteLine(MaxArea(new int[]{ 1,8,6,2,5,4,8,3,7 }));
    }

}
    

