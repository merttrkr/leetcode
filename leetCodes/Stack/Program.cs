using System.Linq.Expressions;

namespace Stack;
public class Solution
{
    /// <summary>
    /// Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
    /// Open brackets must be closed by the same type of brackets.
    /// Open brackets must be closed in the correct order.
    /// Every close bracket has a corresponding open bracket of the same type.
    /// </summary>
    static bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();
        var pairs = new Dictionary<char, char> { { ')', '(' }, { '}', '{' }, { ']', '[' } };
        foreach (var c in s)
        {
            if(pairs.ContainsKey(c)){
                if (stack.Pop() != pairs[c] )
                {
                    return false;
                }
            }
            else
            {
                stack.Push(c);
            }
        }
        return stack.Count == 0;
    }

    public class MinStack
    {
        private Stack<int> stack;
        private Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int val)
        {
            stack.Push(val);
            if (minStack.Count == 0 || val <= minStack.Peek())
            {
                minStack.Push(val);
            }
        }

        public void Pop()
        {
            if (stack.Peek() == minStack.Peek())
            {
                minStack.Pop();
            }
            stack.Pop();
        }

        public int Top()
        {
            return stack.Peek();
        }

        public int GetMin()
        {
            return minStack.Peek();
        }
    }
    /// <summary>
    /// You are given an array of strings tokens that represents an arithmetic expression in a Reverse Polish Notation.
    ///Evaluate the expression.Return an integer that represents the value of the expression.
    /// </summary>
    public static int EvalPRN(string[] tokens)
    {
        Stack<int> stack = new Stack<int>();
        Dictionary<string, Func<int, int, int>> dict = new()
        {
            {"+", (x,y)=>x+y },
            {"-", (x,y)=>x-y },
            {"*", (x,y)=>x*y },
            {"/", (x,y)=>x/y },

        };
        foreach (var item in tokens)
        {
            if (int.TryParse(item, out int result))
            {
                stack.Push(result);
            }
            else
            {
                if(stack.Count < 2)
                {
                    throw new InvalidOperationException("Invalid expression: not enough operands");
                }
                else if(dict.ContainsKey(item))
                {
                    var operand1 = stack.Pop();
                    var operand2 = stack.Pop();
                    stack.Push(dict[item](operand1, operand2));
                }
                else
                {
                    throw new FormatException($"Invalid token: {item}");
                }
            }
        }
        // The final result should be the only item in the stack
        if (stack.Count != 1)
        {
            throw new InvalidOperationException("Invalid expression: too many operands");
        }

        return stack.Pop();
    }
    public static IList<string> GenerateParenthesis(int n)
    {
        IList<string> result = new List<string>();
        BackTrackParanthesis(result,n,"",0,0);
        return result;
    }
    public static void BackTrackParanthesis(IList<string> result,int max,string current,int open,int closed )
    {

        if(current.Length == max*2)
        {
            result.Add(current);
            return;
        }
        if(open < max)
        {
            BackTrackParanthesis(result,max,current+"(",open+1,closed);
        }
        if(closed < open)
        {
            BackTrackParanthesis(result,max,current+")",open,closed+1);
        }

    }
    /// <summary>
    /// Given an array of integers temperatures represents the daily temperatures, return an array answer such that
    /// answer[i] is the number of days you have to wait after the ith day to get a warmer temperature. If there is no future
    /// day for which this is possible, keep answer[i] == 0 instead.
    /// Input: temperatures = [73,74,75,71]
    /// Output: [1,1,4,2]
    /// </summary>
    public static int[] DailyTemperatures(int[] temperatures)
    {
        Stack<int> stack = new Stack<int>();
        int[]result = new int[temperatures.Length];

        for (int i = 0; i < temperatures.Length; i++) 
        {
            while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i])
            {
                int index = stack.Pop();
                result[index] = i - index;
            }
            stack.Push(i);
        }
        return result;
    }
    public static void Main()
    {
        //MinStack minStack = new MinStack();
        //minStack.Push(-2);
        //minStack.Push(0);
        //minStack.Push(-1);
        //minStack.GetMin(); // return -3
        //minStack.Top();    // return 0
        //minStack.Pop();
        //minStack.GetMin(); // return -2
        //Console.WriteLine(string.Join(",",GenerateParenthesis(2)));
        Console.WriteLine(string.Join(",",DailyTemperatures(new int[] {73,74,75,71,69,72,76,73})));
    }
}