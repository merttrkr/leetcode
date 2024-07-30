﻿using System.Linq.Expressions;

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
        public static void Main()
    {
        MinStack minStack = new MinStack();
        minStack.Push(-2);
        minStack.Push(0);
        minStack.Push(-1);
        minStack.GetMin(); // return -3
        minStack.Top();    // return 0
        minStack.Pop();
        minStack.GetMin(); // return -2
    }
}