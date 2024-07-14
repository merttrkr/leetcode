using System.Diagnostics;

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
    public static void Main(string[] args)
    {
        //QueueLengthOfLongestSubstring("asdasda");

        //QueueAndSetLengthOfLongestSubstring("asdasda");

        //HashLengthOfLongestSubstring("asdasda");
        Console.WriteLine(CharacterReplacement("ABBB", 0));
    }
}   
