using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Node<T>
    {
        private T data;
        private Node<T> next;
        public T Data
        {
            get { return data; }
            set {data = value; }
        }
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    
        public Node(T data)
        {
            this.data = data;
            
        } 
        public Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }
    }
    

}
