using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExample
{
    class Queue<T>
    {
        // declare private int variable _capacity
        private int _capacity;
        // declare public int property Capacity
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        // declare private int variable _length
        private int _length;
        // declare public int property Length
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        // declare private generic variable _elements
        private T[] _elements;
        // declare protected generic property _elements 
        protected T[] Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
        // declare private int variable _frontIndex
        private int _frontIndex;
        // declare public int property FrontIndex
        public int FrontIndex
        {
            get { return _frontIndex; }
            set { _frontIndex = value; }
        }
        // delcare public int  property BackIndex
        public int BackIndex
        {// process get (FrontIndex + Length) % Capacity to BackIndex whenever access BackIndex
            get { return (FrontIndex + Length) % Capacity; }
        }

        // Constructor Queue() 
        public Queue()
        {// creat a generic with capacity of Capacity and Elements refer to this generic 
         // Capacity has  default value 0
            Elements = new T[Capacity];
        }
        // Constructor Queue(int capacity)
        public Queue(int capacity)
        {// assign capacity to Capacity 
            Capacity = capacity;
         // creat a generic with capacity of Capacity and Elements refer to this generic 
            Elements = new T[Capacity];
        }

        //public method Enqueue(T element) 
        // T element can be  any data type

        public void Enqueue(T element)
        {
            // if Queue is full, invoke method IncreaseCapacity()
            if (this.Length == Capacity)
            {
                IncreaseCapacity();
            }
            // if Queue is not full, assign element to Elements[] and enlong Length
            // BackIndex change 
            Elements[BackIndex] = element;
            Length++;
        }

        // public method Dequeue. it  has generic output
        public T Dequeue()
        {
            // use if statement to deal with Queue is empty 
            if (this.Length < 1)
            {
                // throw an instance of InvalidOperationException 
                throw new InvalidOperationException("Queue is empty");
            }
            // assign Elements[FrontIndex] to element  but what's the initial value of FrontIndex???
            T element = Elements[FrontIndex];
            // assign default value to Elements[FrontIndex]
            Elements[FrontIndex] = default(T);
            // shorten the Length
            Length--;
            // assign new value to FrontIndex
            FrontIndex = (FrontIndex + 1) % Capacity;
            // return element
            return element;
        }

        // protected member function will allow child class to use 
        // in this program we don't have child class. ??
        protected void IncreaseCapacity()
        {
            // 
            this.Capacity++;
            // twice the Capacity 
            this.Capacity *= 2;
            // creat a object Queue<T> with twice capacity 
            // tempQueue refer to Queue<>()
            Queue<T> tempQueue = new Queue<T>(this.Capacity);
            // use while loop to put data into tempQueue
            while (this.Length > 0)
            {
                tempQueue.Enqueue(this.Dequeue());
            }
            // Elements refer to tempQueue.Elements
            this.Elements = tempQueue.Elements;
            // Length refer to tempQueue.Length
            this.Length = tempQueue.Length;
            // FrontIndex refer to tempQueue.FrontIndex 
            this.FrontIndex = tempQueue.FrontIndex;
        }
    }

    class program
    {
        static void Main(string[] args)
        {
            // declare a int generic Queue named myQueue and creat a int generic Queue object
            // myQueue refer to this object???
            Queue<int> myQueue = new Queue<int>();
            // use for loop to enqueue 0-49 to myQueue and console myQueue.Length
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("Enqueue:" + i);
                myQueue.Enqueue(i);
                Console.WriteLine("New Length is: " + myQueue.Length);

            }
           // use for loop to dequeue 0-49 of myQueue and console myQueue.Length 
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