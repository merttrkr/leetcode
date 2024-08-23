using System.Diagnostics.Metrics;

public class ReverseLinkedListRecursive
{

    /* Link list node */
    public class Node
    {
        public int data;
        public Node next;

        public Node(int nodeData)
        {
            this.data = nodeData;
            this.next = null;
        }
    }

    class LinkedList
    {
        public Node head;

        public LinkedList()
        {
            this.head = null;
        }

        public void insertNode(int nodeData)
        {
            Node node = new Node(nodeData);

            if (this.head != null)
            {
                node.next = head;
            }
            this.head = node;
        }
    }

    /* Function to print linked list */
    public static void printSinglyLinkedList(Node node,
                        String sep)
    {
        while (node != null)
        {
            Console.Write(node.data + sep);
            node = node.next;
        }
    }

    // Complete the reverse function below.
    static Node reverse(Node head)
    {
        if (head == null)
        {
            return head;
        }

        if (head.next == null)
        {
            return head;
        }
        // 1 2 3 4 5
        // 4 5
        Node newHead = reverse(head.next);
        head.next.next = head;
        head.next = null;

        return newHead;
    }
    static Node reverse2(Node current,Node previous)
    {
        if(current == null)
        {
            return previous;
        }
        // 1 2 3 4 5
        // current is 1 
        // current -> 2
        // 2 3 4 5
        // the previous one should point to current
        Node next = current.next;
        current.next = previous;
        return reverse2(next, current);
    }
    static Node ReverseListTail(Node head)
    {
        return reverse2(head,null);
    }
    static Node reverseIterative(Node current)
    {
        Node previous = null;
        while (current != null)
        {
            Node next = current.next;
            current.next = previous;
            previous = current;
            current = next;
        }
        return previous;
    }
    public static Node MergeTwoListsSorted(Node list1, Node list2)
    {
        Node dummy = new Node(-1);
        Node trev = dummy;

        while (list1 != null && list2 != null)
        {
            if(list1.data <= list2.data)
            {
                trev.next = list1;
                list1 = list1.next;
            }
            else
            {
                trev.next = list2;
                list2 = list2.next;
            }
            trev = trev.next;
        }
        // Append the remaining elements from the non-empty list
        if (list1 != null)
        {
            trev.next = list1;
        }
        else
        {
            trev.next = list2;
        }

        // The head of the merged list is the next node after the dummy
        return dummy.next;
    }
    
    public void ReorderList(Node head)
    {
        if (head == null || head.next == null) return;

        // Step 1: Count the size of the list and push all nodes onto a stack
        int size = 0;
        Node trev = head;
        Stack<Node> stack = new Stack<Node>();
        while (trev != null)
        {
            size++;
            stack.Push(trev);
            trev = trev.next;
        }

        // Step 2: Reorder the list by merging from both ends
        Node current = head;
        for (int i = 0; i < size / 2; i++)
        {
            Node endNode = stack.Pop();

            endNode.next = current.next;
            current.next = endNode;

            // Move current to the next node to be processed
            current = endNode.next;
        }

        // Step 3: Ensure the last node points to null to avoid cycles
        current.next = null;

    }

    /// <summary>
    /// Given the head of a linked list, remove the nth node from the end of the list and return its head.
    /// 1 2 3 4 5
    /// </summary>
    public static Node RemoveNthFromEnd(Node head, int n)
    {
        Node fast = head, slow = head;
        for (int i = 0; i < n; i++) fast = fast.next;
        if (fast == null) return head.next;
        while (fast.next != null)
        {
            fast = fast.next;
            slow = slow.next;
        }
        slow.next = slow.next.next;
        return head;
    }
    // Driver code
    public static void Main(String[] args)
    {
        LinkedList llist = new LinkedList();
        LinkedList llist2 = new LinkedList();

        llist.insertNode(5);
        llist.insertNode(4);
        llist.insertNode(3);
        llist.insertNode(2);
        llist.insertNode(1);


        llist2.insertNode(1);
        llist2.insertNode(4);
        llist2.insertNode(5);
        llist2.insertNode(6);


        printSinglyLinkedList(llist.head, " ");

        Console.WriteLine();
        //Console.WriteLine("Reversed Linked list:");
        //Node llist1 = reverseIterative(llist.head);
        //Node llist3 = reverse(llist2.head);
        Node llist1 = RemoveNthFromEnd(llist.head,2);
        printSinglyLinkedList(llist1, " ");

        //printSinglyLinkedList(MergeTwoLists(llist1, llist3), " ");


    }
}

