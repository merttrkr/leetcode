using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArraysAndHashing;

public class Solution
{
    public static bool ContainsDuplicate(int[] nums)
    {
        Dictionary<int,int> dict = new Dictionary<int, int>();
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

        Dictionary<char, int> charCount = new Dictionary<char, int>();

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
    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int,int> keyValuePairs = new Dictionary<int,int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int searchResult = target - nums[i];
            if (keyValuePairs.ContainsKey(searchResult))
            {
                return new int[] { keyValuePairs[searchResult],i };
            }
            keyValuePairs.TryAdd(nums[i], i);
        }
        return null;
    }

    public static int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int,int> result = nums.GroupBy(x => x).ToDictionary(num => num.Key, num => num.Count());
        return result.OrderByDescending(x => x.Value).Take(k).Select(x => x.Key).ToArray();
        
    }
    public static void Main(string[] args)
    {
        int[] nums = { 1,2,3,4,5 };
        string s = "ana";
        string[] strs = { "eat", "tea", "tan", "ate", "nat", "bat" };

        //Console.WriteLine(ContainsDuplicate(nums));
        //Console.WriteLine(IsAnagram(s, s));
        //GroupAnagrams(strs);
        int[] arr = TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2);
        foreach (var item in arr)
        {
            Console.WriteLine(item);
        }



    }

}
    

