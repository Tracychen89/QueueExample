using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExample
{
    class Queue<T>
    {
        // Properties private _capacity
        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        // properties private int _length
        private int _length;
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        // properties private generic _elements 
        private T[] _elements;
        protected T[] Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
        // properties private int _frontIndex
        private int _frontIndex;
        public int FrontIndex
        {
            get { return _frontIndex; }
            set { _frontIndex = value; }
        }
        // properties public int BackIndex
        public int BackIndex
        {
            get { return (FrontIndex + Length) % Capacity; }
        }

        // Constructor Queue() 
        public Queue()
        {
            Elements = new T[Capacity];
        }
        // Constructor Queue(int capacity)
        public Queue(int capacity)
        {
            Capacity = capacity;
            Elements = new T[Capacity];
        }

        //public method Enqueue(T element) 
        //why argument should be T???

        public void Enqueue(T element)
        {
            // if Queue is full, invoke method IncreaseCapacity()
            // keyword this???
            if (this.Length == Capacity)
            {
                IncreaseCapacity();
            }
            // if Queue is not full, assign element to Elements[] and enlong Length
            Elements[BackIndex] = element;
            Length++;
        }

        // public method Dequeue. it  has generic output????
        public T Dequeue()
        {
            // use if statement to deal with Queue is empty 
            if (this.Length < 1)
            {
                // throw an instance of InvalidOperationException 
                throw new InvalidOperationException("Queue is empty");
            }

            T element = Elements[FrontIndex];
            Elements[FrontIndex] = default(T);
            Length--;
            FrontIndex = (FrontIndex + 1) % Capacity;
            return element;
        }

        // protected member function will allow child class to use 
        // in this program we don't have child class. ??
        // keyword this???
        protected void IncreaseCapacity()
        {
            // ???
            this.Capacity++;
            // twice the Capacity 
            this.Capacity *= 2;
            // instanciate tempQueue as  Queue<T> with twice capacity 
            Queue<T> tempQueue = new Queue<T>(this.Capacity);
            // while loop ???
            while (this.Length > 0)
            {
                tempQueue.Enqueue(this.Dequeue());
            }
            // assign tempQueue Elements to Elements
            this.Elements = tempQueue.Elements;
            // assign tempQueue Length to Length
            this.Length = tempQueue.Length;
            // assign tempQueue FrontIndex to FrontIndex 
            this.FrontIndex = tempQueue.FrontIndex;
        }
    }

    class program
    {
        static void Main(string[] args)
        {
            Queue<int> myQueue = new Queue<int>();
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("Enqueue:" + i);
                myQueue.Enqueue(i);
                Console.WriteLine("New Length is: " + myQueue.Length);

            }
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("Dequeue: " + myQueue.Dequeue());
                Console.WriteLine("New Length is: " + myQueue.Length);
            }
            try
            {
                myQueue.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("As expected I received this error: " + ex.Message);
            }

        }
    }
}