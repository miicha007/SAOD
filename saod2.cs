using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD2
{
    class Program
    {
        class myList<T>
        {
            private int count;
            const int n = 10;
            public T[] data;
            public int Size
            {
                get { return count; }
            }
            public bool IsEmpty
            {
                get { return count == 0; }
            }
            public myList()
            {
                data = new T[n];

            }
            public myList(int count)
            {
                data = new T[count];

            }
            private void Resize(int max)
            {
                T[] tempData = new T[max];
                for (int i = 0; i < count; i++)
                    tempData[i] = data[i];
                data = tempData;
            }
            public void Add(T x)
            {
                if (count == data.Length)
                {
                    Resize(data.Length * 2);
                }
                data[count++] = x;
            }
            public T Last
            {
                get
                {
                    if (count == 0) return default(T);
                    else return data[count - 1];
                }
            }
            public T First
            {
                get
                {
                    if (count == 0) return default(T);
                    else return data[0];
                }
            }
            public void Clear()
            {
                count = default;
                data = new T[n];
            }
            public void Insert(int i, T value)
            {
                exception(i);
                if (count == data.Length)
                {
                    Resize(count * 2);
                    Console.WriteLine("resize");
                }
                count++;
                for (int j = count-1; j > i; j--)
                {
                    data[j] = data[j - 1];
                }
                data[i] = value;
            }
            public void exception(int i)
            {
                if (i >= count) throw new ArgumentOutOfRangeException();
            }
            public void RemoveAt(int i)
            {
                exception(i);
                count--;
                for (; i < count; i++)
                {
                    data[i] = data[i + 1];
                    Console.WriteLine("XXXXXX");
                }
                Resize(count);
            }
            public T this[int key]
            {
                get
                {
                    exception(key);
                    return data[key];
                }
                set
                {
                    exception(key);
                    data[key] = value;
                }
            }
            public void ForEach(Action<T> action)
            {
                foreach(T i in data)
                {
                    action(i);
                }
            }
            public T Find(Predicate<T> match)
            {
                foreach (T i in data)
                {
                    if (match(i)) return i;
                }
                return default;
            }
            public int FindIndex(Predicate<T> match)
            {
                for(int i = 0; i < data.Length; i++)
                {
                    if (match(data[i])) return i;
                }
                return -1;
            }
        }
        static void Main(string[] args)
        {
            myList<int> mas = new myList<int>();
            for(int i = 0; i<1; i++)
            {
                mas.Add(i);
            }
            mas.Add(4);
            mas.Insert(1, 88);
            mas.RemoveAt(2);
            Console.WriteLine(mas[1].ToString()+"ss");
            //mas.RemoveAt(3);
            //mas.Insert(0, 88);
            //mas.Insert(mas.Size, 88);
            for (int i=0;i< mas.Size; i++)
            {
                Console.WriteLine("{0}-{1},", i, mas.data[i]);
            }
            Console.ReadKey();
        }
    }
}
