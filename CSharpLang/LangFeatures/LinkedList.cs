using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangFeatures
{
    public class MyLinkedList<T>
    {
        private Node? _head = null;
        private Node? _tail = null;

        public T Append(T item)
        {
            if (_head == null)
            {
                _head = new Node(item);
                _tail = _head;
            }
            else
            {
                _tail = _tail.Append(item);
            }

            return _tail._value;
        }

        public T Get(int position)
        {
            Node get = _head;

            for (int i = 0; i <= position && get._next != null; i++)
            {
                get = get._next;
            }

            return get._value;
        }

        public void Insert(T value, int position)
        {
            var newNode = new Node(value);
            Node insertHere = _head;
            Node previous = null;

            for (int i = 0; i < position && insertHere._next != null; i++)
            {
                previous = insertHere;
                insertHere = insertHere._next;
            }

            newNode._next = insertHere;
            if (previous != null)
                previous._next = newNode;
            else 
                _head = newNode;
        }

        public void Remove(int position)
        {
            Node removeHere = _head;
            Node previous = _head;

            for (int i = 0; i < position && removeHere._next != null; i++)
            {
                previous = removeHere;
                removeHere = removeHere._next;
            }

            previous._next = removeHere._next;
        }

        public void Reverse()
        {
            var a = _head;
            var b = _head._next;
            _tail = _head;

            while (b != null)
            {
                Node c = b._next;
                b._next = a;

                a = b;

                b = c;
            }

            _head = a;
            _tail._next = null;
        }

        public override string ToString()
        {
            if (_head != null)
            {
                StringBuilder sb = new(_head._value.ToString());
                Node next = _head._next;
                while (next != null)
                {
                    sb.Append(next._value.ToString());
                    next = next._next;
                }
                return sb.ToString();
            }
            return "<Empty>";
        }

        private class Node
        {
            public T _value;

            public Node? _next;

            public Node(T item)
            {
                _value = item;
            }

            public Node Append(T next)
            {
                _next = new Node(next);
                return _next;
            }
        }
    }
}
