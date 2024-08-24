using System.Diagnostics.Metrics;

public class ReverseLinkedListRecursive
{
    
// Definition for a ListNode.
    

    /* Link list node */
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode random;

        public ListNode(int nodeData)
        {
            this.val = nodeData;
            this.next = null;
            this.random = null;

        }
    }

    class LinkedList
    {
        public ListNode head;

        public LinkedList()
        {
            this.head = null;
        }

        public void insertNode(int nodeData)
        {
            ListNode node = new ListNode(nodeData);

            if (this.head != null)
            {
                node.next = head;
            }
            this.head = node;
        }
    }

    /* Function to print linked list */
    public static void printSinglyLinkedList(ListNode node,
                        String sep)
    {
        while (node != null)
        {
            Console.Write(node.val + sep);
            node = node.next;
        }
    }

    // Complete the reverse function below.
    static ListNode reverse(ListNode head)
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
        ListNode newHead = reverse(head.next);
        head.next.next = head;
        head.next = null;

        return newHead;
    }
    static ListNode reverse2(ListNode current,ListNode previous)
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
        ListNode next = current.next;
        current.next = previous;
        return reverse2(next, current);
    }
    static ListNode ReverseListTail(ListNode head)
    {
        return reverse2(head,null);
    }
    static ListNode reverseIterative(ListNode current)
    {
        ListNode previous = null;
        while (current != null)
        {
            ListNode next = current.next;
            current.next = previous;
            previous = current;
            current = next;
        }
        return previous;
    }
    public static ListNode MergeTwoListsSorted(ListNode list1, ListNode list2)
    {
        ListNode dummy = new ListNode(-1);
        ListNode trev = dummy;

        while (list1 != null && list2 != null)
        {
            if(list1.val <= list2.val)
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
    
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null) return;

        // Step 1: Count the size of the list and push all nodes onto a stack
        int size = 0;
        ListNode trev = head;
        Stack<ListNode> stack = new Stack<ListNode>();
        while (trev != null)
        {
            size++;
            stack.Push(trev);
            trev = trev.next;
        }

        // Step 2: Reorder the list by merging from both ends
        ListNode current = head;
        for (int i = 0; i < size / 2; i++)
        {
            ListNode endNode = stack.Pop();

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
    public static ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        ListNode fast = head, slow = head;
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
    public static ListNode CopyRandomList(ListNode head)
    {
        if (head == null) return null;

        // Step 1: Interweave the original list with the copied nodes
        ListNode ptr = head;
        while (ptr != null)
        {
            ListNode newNode = new ListNode(ptr.val);
            newNode.next = ptr.next;
            ptr.next = newNode;
            ptr = newNode.next;
        }

        // Step 2: Assign random pointers for the copied nodes
        ptr = head;
        while (ptr != null)
        {
            if (ptr.random != null)
            {
                ptr.next.random = ptr.random.next;
            }
            ptr = ptr.next.next;
        }

        // Step 3: Unweave the lists to restore the original list and extract the copied list
        ptr = head;
        ListNode dummy = new ListNode(0);
        ListNode copy, copyPtr = dummy;

        while (ptr != null)
        {
            ListNode front = ptr.next.next;

            // Extract the copy
            copy = ptr.next;
            copyPtr.next = copy;
            copyPtr = copy;

            // Restore the original list
            ptr.next = front;
            ptr = front;
        }

        return dummy.next;

    }
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int baseTen = 1;
        ListNode trevl1 = l1;
        ListNode trevl2 = l2;
        int sumL1 = 0;
        int sumL2 = 0;

        // Convert the first linked list to its corresponding integer value
        while (trevl1 != null)
        {
            sumL1 += baseTen * trevl1.val;
            baseTen *= 10;
            trevl1 = trevl1.next;
        }

        baseTen = 1; // Reset baseTen for the second linked list

        // Convert the second linked list to its corresponding integer value
        while (trevl2 != null)
        {
            sumL2 += baseTen * trevl2.val;
            baseTen *= 10;
            trevl2 = trevl2.next;
        }

        // Sum the two integers
        int totalSum = sumL1 + sumL2;

        // Create a new linked list for the result
        ListNode head = null;
        ListNode current = null;

        // If the total sum is 0, return a node with value 0
        if (totalSum == 0)
        {
            return new ListNode(0);
        }

        // Convert the sum back to a linked list
        while (totalSum > 0)
        {
            int digit = totalSum % 10;
            ListNode newNode = new ListNode(digit);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                current.next = newNode;
            }
            current = newNode;
            totalSum /= 10;
        }

        return head;
    }
    public static ListNode AddTwoNumbersShortVersion(ListNode l1, ListNode l2)
    {
        ListNode dummy = new ListNode(-1);
        ListNode current = dummy;
        int carry = 0;

        while (l1 != null || l2 != null)
        {
            int currentDataL1 = l1 != null ? l1.val : 0;
            int currentDataL2 = l2 != null ? l2.val : 0;
            int sum = currentDataL1 + currentDataL2 + carry;
            carry = sum / 10;
            current.next = new ListNode(sum % 10);
            current = current.next;

            if (l1 != null) l1 = l1.next;
            if (l2 != null) l2 = l2.next;
        }

        if (carry > 0)
        {
            current.next = new ListNode(carry);
        }

        return dummy.next;
    }
    public static ListNode DeleteDuplicatesInSortedList(ListNode head)
    {
        if (head == null) return null;

        ListNode current = head;

        while (current != null && current.next != null)
        {
            if (current.val == current.next.val)
            {
                current.next = current.next.next;  // Remove the duplicate
            }
            else
            {
                current = current.next;  // Move to the next node
            }
        }

        return head;
    }
    public static ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null) return null;
        ListNode current = head;
        HashSet<int> visited = new HashSet<int>(current.val);
        while (current != null && current.next !=null)
        {
            if (visited.Contains(current.next.val))
            {
                current.next = current.next.next;
            }
            else
            {
                visited.Add(current.next.val);
                current= current.next;
            }
        }
        return head;
    }
    // Driver code
    public static void Main(String[] args)
    {
        LinkedList llist = new LinkedList();
        LinkedList llist2 = new LinkedList();

        llist.insertNode(4);
        llist.insertNode(3);
        llist.insertNode(2);
        llist.insertNode(2);
        llist.insertNode(2);
        llist.insertNode(1);


        llist2.insertNode(4);
        llist2.insertNode(6);
        llist2.insertNode(5);



        printSinglyLinkedList(llist.head, " ");

        Console.WriteLine();
        //Console.WriteLine("Reversed Linked list:");
        //ListNode llist1 = reverseIterative(llist.head);
        //ListNode llist3 = reverse(llist2.head);
        //ListNode llist1 = RemoveNthFromEnd(llist.head,2);
        //printSinglyLinkedList(llist1, " ");

        //printSinglyLinkedList(MergeTwoLists(llist1, llist3), " ");
        //printSinglyLinkedList(AddTwoNumbers(llist.head, llist2.head)," ");
        //AddTwoNumbersShortVersion(llist.head, llist2.head);
        printSinglyLinkedList(DeleteDuplicates(llist.head), " ");
    }
}

