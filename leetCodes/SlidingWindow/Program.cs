using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace SlidingWindow;
public class Solution
{
    public static int MaxProfit(int[] prices)
    {
        int minPrice = int.MaxValue;
        int maxProfit = 0;
        for (int i = 0; i < prices.Length; i++)
        {
            minPrice = minPrice > prices[i] ? prices[i] : minPrice;
            maxProfit = prices[i] - minPrice > maxProfit ? prices[i] - minPrice : maxProfit;
        }
        return maxProfit;
    }
    public static int QueueLengthOfLongestSubstring(string s)
    {
        Queue<char> queue = new Queue<char>();

        int maxLength = 0;
        int counter = 0;
        foreach (char c in s)
        {
            while (queue.Contains(c))
            {
                queue.Dequeue();
                counter--;
            }
            queue.Enqueue(c);
            counter++;

        if (counter > maxLength) maxLength = counter;
        }
        return maxLength;

    }
    public static int QueueAndSetLengthOfLongestSubstring(string s)
    {
        HashSet<char> set = new HashSet<char>();
        Queue<char> queue = new Queue<char>();
        int maxLength = 0;

        foreach (char c in s)
        {
            while (set.Contains(c))
            {
                char removed = queue.Dequeue();
                set.Remove(removed);
            }
            queue.Enqueue(c);
            set.Add(c);
            maxLength = Math.Max(maxLength, queue.Count);
        }

        return maxLength;
    }

    public static int HashLengthOfLongestSubstring(string s)
    {
        HashSet<char> set = new HashSet<char>();
        int maxLength = 0;
        int left = 0; // Pointer for the start of the current substring

        for (int right = 0; right < s.Length; right++)
        {
            char currentChar = s[right];

            // If currentChar is already in the set, move left pointer to right until no duplicates
            while (set.Contains(currentChar))
            {
                set.Remove(s[left]);
                left++;
            }

            // Add currentChar to the set
            set.Add(currentChar);

            // Update maxLength if the current substring length is longer
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    public static int CharacterReplacement(string s, int k)
    {
        int len = s.Length;
        int start = 0;
        int maxLength = 0;
        int maxCount = 0;
        int[] frequency = new int[26];
        for(int end = 0; end < len; end++)
        {
            maxCount = Math.Max(++frequency[s[end] - 'A'], maxCount);
            while(end - start + 1 > maxCount + k)
            {
                frequency[s[start]- 'A']--;
                start++;
            }
            maxLength = Math.Max(maxLength, end-start+1);
        }
        return maxLength;
    }

    public static bool CheckInclusion(string s1, string s2)
    {
        // If s1 is longer than s2, return false
        if (s1.Length > s2.Length)
            return false;

        // Create frequency maps for s1 and the first window in s2
        int[] s1map = new int[26];
        int[] s2map = new int[26];

        // Fill s1map with frequencies of characters in s1
        for (int i = 0; i < s1.Length; i++)
        {
            s1map[s1[i] - 'a']++;
            s2map[s2[i] - 'a']++; // Fill the first window of s2map
        }

        // Check if the first window matches
        if (Matches(s1map, s2map))
            return true;

        // Now slide the window over s2
        for (int i = s1.Length; i < s2.Length; i++)
        {
            // Add the next character in s2 to the window
            s2map[s2[i] - 'a']++;
            // Remove the character that is sliding out of the window
            s2map[s2[i - s1.Length] - 'a']--;

            // Check if the updated window matches
            if (Matches(s1map, s2map))
                return true;
        }

        return false;
    }
    // Helper method to compare frequency maps
    public static bool Matches(int[] s1map, int[] s2map)
    {
        for (int i = 0; i < 26; i++)
        {
            if (s1map[i] != s2map[i])
                return false;
        }
        return true;
    }
    /// <summary>
    /// Given two strings s1 and s2, return true if s2 contains a permutation of s1, or false otherwise.
    /// </summary>
    /// <param name="s1">ab</param>
    /// <param name="s2">eidbaooo</param>
    /// <returns>true</returns>
    public static bool CheckInclusionTest(string s1, string s2)
    {
        // Create a frequency map for characters in s1
        int[] charFrequencyMap = new int[26];
        foreach (char c in s1)
        {
            charFrequencyMap[c - 'a']++;
        }

        // Initialize pointers and necessary variables
        int left = 0, right = 0;
        int size = s1.Length;
        int requiredChars = size;
        int currentMatchCount = 0;
        bool stop = false;
        // Traverse s2 with the right pointer
        while (right < s2.Length)
        {
            if (charFrequencyMap[s2[right] - 'a'] > 0)
            {
                currentMatchCount++;
            }
            if (currentMatchCount == size)
            {
                return true;
            }
            charFrequencyMap[s2[right] - 'a']--;
            right++;
            while (currentMatchCount>0)
            {
                if (charFrequencyMap[s2[left]] > 0)
                {
                    currentMatchCount--;
                }
                charFrequencyMap[s2[left]]--;
                left++;
            }
        }

        // If no valid window is found, return false
        return false;
    }


    /// <summary>
    ///Explanation: Both 'a's from t must be included in the window.
    ///Since the largest window of s only has one 'a', return empty string.
    /// Input: s = "ADOBECODEBANC", t = "ABC"
    ///Output: "BANC"
    /// </summary>
    public static string MinWindow(string s, string t)
    {
        if (t.Length>s.Length)
        {
            return "";
        }
        int[] freq = new int[128];

        for(int i=0; i<t.Length; i++)
        {
            freq[t[i]]++;
        }
        int left = 0;
        int right = 0;
        int requiredChars = t.Length;
        int minWindow = int.MaxValue;
        int windowStart = 0;

        while (right < s.Length)
        {
            if (freq[s[right]] > 0)
            {
                requiredChars--;
            }
            freq[s[right]]--;
            right++;
            while (requiredChars == 0)
            {
                if( minWindow > right-left)
                {
                    windowStart = left;
                    minWindow = right - left;
                }
                if (freq[s[left]] == 0)
                {
                    requiredChars++;
                }
                freq[s[left]]++;
                left++;
            }
        }
        return minWindow == int.MaxValue ? "" : s.Substring(windowStart, minWindow);
    }
    
    public static int ContinuesSum(int[] freq,int target)
    {
        int left = 0;
        int right = 0;
        int sum = 0;
        int minCount = int.MaxValue;
        for (; right < freq.Length; right++)
        {
            sum += freq[right];
            while (sum >= target)
            {
                minCount = Math.Min(minCount, right - left+1);
                sum-= freq[left];
                left++;
            }
        }
        return sum == 0? -1: minCount ;
    }
    

    public static void Main(string[] args)
    {
        //QueueLengthOfLongestSubstring("asdasda");

        //QueueAndSetLengthOfLongestSubstring("asdasda");

        //HashLengthOfLongestSubstring("asdasda");
        //Console.WriteLine(CharacterReplacement("ABBB", 0));
        //Console.WriteLine(CheckInclusionTest("adce", "asxndcecad"));
        //Console.WriteLine(continuesSum(new int[] { 5, 1, 3, 5, 10, 7, 4 }, 15));
        //Console.WriteLine(MinWindow("ADOBECODEBANC", "ABC"));
    }
}   
