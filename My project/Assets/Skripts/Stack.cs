using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace lab3
{
    public class Stack<T> 
    {
        private LinkedList<T> list;
        public  Stack()
        {
            list = new LinkedList<T>();
        }
        public void Push(T data)
        {
            list.AddFirst(data);
        }
        public T Pop()
        {
            return list.RemoveFirst();
        }
        public T Top()
        {
            if (list.Count == 0)
            {
                return default(T);
            }
            return list.Head;
        }
        public bool IsEmpty()
        {
            return (list.Count == 0);
        }
        public List<string> Print()
        {
            return list.GetList();
        }
    }
}
