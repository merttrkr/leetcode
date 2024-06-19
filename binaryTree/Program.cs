

// C# code for the above approach
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetCodes;

public class BinaryTree
{
    public TreeNode root;

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    public static TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
        {
            return null;
        }
        InvertTree(root.left);
        InvertTree(root.right);

        TreeNode temp = root.left;
        root.left = root.right;
        root.right = temp;

        return root;
    }


    public static int MaxDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        var leftMaxDepth = MaxDepth(root.left);
        var rightMaxDepth = MaxDepth(root.right);

        return Math.Max(leftMaxDepth, rightMaxDepth) + 1;
    }
    public static void Main(string[] args)
    {
        // Construct a tree shown in the above figure
        BinaryTree tree = new BinaryTree();
        tree.root = new TreeNode(1);
        tree.root.left = new TreeNode(2);
        tree.root.right = new TreeNode(3);
        tree.root.right.left= new TreeNode(6);
        tree.root.right.right = new TreeNode(7);

        //InvertTree(tree.root);
        Console.WriteLine("final depth: "+ MaxDepth(tree.root));


    }
}
// Driver code
