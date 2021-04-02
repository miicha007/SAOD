using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD6
{


    class Node<T>
    {
        public Node<T> Right { get; set; }
        public Node<T> Left { get; set; }


        public T Data { get; set; }
        public Node(T value)
        {
            Data = value;
        }

    }
    class Btree<T>
    {
        int count = 0;
        Node<T> head;

        public int Count { get; }
        public T Head { get { return head.Data; } }



        public delegate int Compare(T x, T y);

        public Compare compare;

        //public Btree(Func<T,T,int> comparer, Func<T,int> lens)
        //{
        //    compare = new Btree<T>.Compare(comparer);
        //    len = new Len(lens);
        //    head = default;
        //}
        public Btree(Func<T, T, int> comparer)
        {
            compare = new Btree<T>.Compare(comparer);
            head = default;
        }
        public void Insert(T value)
        {
            count++;
            Node<T> N = new Node<T>(value);
            if (head == default)
            {
                head = N;
            }
            else
            {
                Node<T> i = head;
                while (i != default)
                {
                    if (compare(N.Data, i.Data) == 1)
                    {
                        if (i.Right == default)
                        {
                            i.Right = N;
                            break;
                        }
                        i = i.Right;
                    }
                    else
                    {
                        if (i.Left == default)
                        {
                            i.Left = N;
                            break;

                        }
                        i = i.Left;
                    }
                }
                //i = N;
            }
        }
        //public delegate int Len(T x);

        //public Len len;
        //public int lens(Node<T> node) 
        //{
        //    int k = 0;
        //    if (node != default)
        //    {
        //        k += len(node.Data);
        //        if (node.Left != default) k += lens(node.Left);
        //        if (node.Right != default) k += lens(node.Right);
        //    }
        //    return k;
        //}
        public void Clear()
        {
            count = 0;
            head = default;
        }
        //public Node<T> Order(List<Node<T>>)
        //{

        //}
        //public void changeOrder()
        //{

        //}
        private void List(List<T> list, Node<T> node)
        {
            if (node != default)
            {
                List(list, node.Left);
                list.Add(node.Data);
                List(list, node.Right);
            }
        }
        public List<T> GetList()
        {
            List<T> list = new List<T>();
            List(list, head);
            return list;
        }
        public void Str(List<string> list, Node<T> node, int i)
        {
            if (node != default)
            {
                Str(list, node.Left, i + 1);
                string st = "".PadLeft(node.Data.ToString().Length, ' ');
                for (int j = 0; j < list.Count; j++) { if (i != j) { list[j] += st; } else { list[i] += node.Data.ToString(); } }
                Str(list, node.Right, i + 1);
            }
        }
        public void toStringa()
        {
            List<string> list_str = new List<string> { };
            int d = GetHeight();
            for (int i = 0; i < d + 1; i++)
            {
                list_str.Add("");
            }
            Str(list_str, head, 0);
            for (int i = 0; i < list_str.Count; i++)
            {
                Console.WriteLine(list_str[i]);
            }
        }
        public int GetHeight()
        {
            int max = 0;
            Stack<Tuple<Node<T>, int>> stacks = new Stack<Tuple<Node<T>, int>>();
            stacks.Push(new Tuple<Node<T>, int>(head, 1));
            while (stacks.Count > 0)
            {
                var stack = stacks.Pop();
                if (stack.Item1 != default)
                {
                    if (stack.Item2 > max) { max = stack.Item2; }
                    stacks.Push(new Tuple<Node<T>, int>(stack.Item1.Left, stack.Item2 + 1));
                    stacks.Push(new Tuple<Node<T>, int>(stack.Item1.Right, stack.Item2 + 1));

                }
            }
            return max;
        }
        public int LeafCount()
        {
            int leafCount = 0;
            Stack<Node<T>> stacks = new Stack<Node<T>>();
            stacks.Push(head);
            while (stacks.Count > 0)
            {
                var stack = stacks.Pop();
                if (stack != default)
                {
                    if (stack.Left == default && stack.Right == default) { leafCount++; }
                    stacks.Push(stack.Left);
                    stacks.Push(stack.Right);
                }
            }
            return leafCount;
        }
        public T MaxValueItr()
        {
            Node<T> i = head;
            while (true)
            {
                if (i.Right == default)
                {
                    return i.Data;
                }
                i = i.Right;
            }
        }
        public T MinValueItr()
        {
            Node<T> i = head;
            while (true)
            {
                if (i.Left == default)
                {
                    return i.Data;


                }
                i = i.Left;
            }
        }
        private T maxValueRec(Node<T> node)
        {
            if (node.Right == default) { return node.Data; }
            return maxValueRec(node.Right);
        }
        public T MaxValueRec()
        {
            return maxValueRec(head);
        }
        private T minValueRec(Node<T> node)
        {
            if (node.Left == default) { return node.Data; }
            return minValueRec(node.Left);
        }
        public T MinValueRec()
        {
            return minValueRec(head);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int compare1(int x, int y)
            {
                if (x > y) return 1;
                else if (x == y) return 0;
                else return -1;
            }



            Btree<int> btree = new Btree<int>(compare1);
            for (int i = 0; i < 10; i++)
            {
                btree.Insert(r.Next(10));
                //btree.Insert(i);

            }
            Console.WriteLine("deep:" + btree.GetHeight().ToString());
            btree.toStringa();
            Console.WriteLine("MaxValueItr : " + btree.MaxValueItr());
            Console.WriteLine("MaxValueRec : " + btree.MaxValueRec());
            Console.WriteLine("MinValueItr : " + btree.MinValueItr());
            Console.WriteLine("MinValueRec : " + btree.MinValueRec());
            Console.WriteLine("LeafCount : " + btree.LeafCount());
            Console.WriteLine("GetHeight : " + btree.GetHeight());

            





            Console.ReadKey();
        }
    }
}


