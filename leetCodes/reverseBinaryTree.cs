

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

        if (root.left != null && root.right != null)
        {
            var temp = root.left;
            root.left = root.right;
            root.right = temp;
        }
        if (root.left != null)
        {
            InvertTree(root.left);
        }
        if (root.right != null)
        {
            InvertTree(root.right);
        }
        return root;
    }
    public static void Main(string[] args)
    {
        // Construct a tree shown in the above figure
        BinaryTree tree = new BinaryTree();
        tree.root = new TreeNode(1);
        tree.root.left = new TreeNode(2);
        tree.root.right = new TreeNode(3);
        tree.root.left.left = new TreeNode(4);
        tree.root.left.right = new TreeNode(5);
        tree.root.left.right.left = new TreeNode(6);
        tree.root.left.right.right = new TreeNode(7);

        InvertTree(tree.root);


    }
}
// Driver code
