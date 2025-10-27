using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class LinkedList<T>
    {

        private Node<T> head;
        private int count;

        public T Head
        {
            get { return head.Data; }
        }
        public LinkedList()
        {
            head = null;
            count = 0;
        }
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            newNode.Next = head;
            head = newNode;
            count++;
        }
        public T RemoveFirst()
        {
            var temp = head.Data;
            if (head != null)
            {
                head = head.Next;
                count--;
            }
            return temp;
        }

        public List<string> GetList()
        {
            var tempStringList = new List<string>();
            if (head != null)
            {
                Node<T> tempHead = head;
                Console.WriteLine(tempHead.Data.ToString());
                tempStringList.Add(tempHead.Data.ToString());
                
                while (tempHead.Next != null)
                {
                    tempHead = tempHead.Next;
                    tempStringList.Add(tempHead.Data.ToString());
                    

                }

                return tempStringList;
            }
            else
            {
                return(new List<string>());
            }
        }

        public void AddLast(T data)
        {
            if (head == null)
            {
                AddFirst(data);
                return;
            }
            Node<T> newNode = new Node<T>(data);
            Node<T> tempHead = head;

            while (tempHead.Next != null)
            {
                tempHead = tempHead.Next;
            }
            tempHead.Next = newNode;
        }
        public int Count
        {
            get { return count; }
        }
    }
}
