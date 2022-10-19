using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2._1
{

    class PriorityQ
    {
        // list in unsorted order, priority feature provided by remove method
        private int maxSize;
        private List<Neighborhood> queueList;
        private int nItems;
        
        public PriorityQ() // constructor
        {
            queueList = new List<Neighborhood>();
            nItems = 0;
        }
        
        public void Add(Neighborhood item) // insert item
        {
            queueList.Add(item);
            nItems++;
        } // end insert()
         
        public Neighborhood Remove() // remove maximum item
        {
            Neighborhood tempMax = queueList[0];
            foreach (Neighborhood neigbor in queueList)
            {
                if (neigbor.DeliveryList.Count>tempMax.DeliveryList.Count) // true if neigbor's delivery num is greater than temp vari
                {
                    tempMax = neigbor;
                }
            }
            queueList.Remove(tempMax);
            nItems--;
            
            return tempMax;
        }// end Remov()
        
        
        public bool IsEmpty() // true if queue is empty
        { return (nItems == 0); }
       
    }

    class Queue
    {
        private int maxSize;
        private Neighborhood[] queArray;
        private int front;
        private int rear;
        private int nItems;
        
        public Queue(int s) // constructor
        {
            maxSize = s;
            queArray = new Neighborhood[maxSize];
            front = 0;
            rear = -1;
            nItems = 0;
        }

        public void Insert(Neighborhood j) // put item at rear of queue
        {
            if (rear == maxSize - 1) // deal with wraparound
                rear = -1;
            queArray[++rear] = j; // increment rear and insert
            nItems++; // one more item
        }

        public Neighborhood Remove() // take item from front of queue
        {
            Neighborhood temp = queArray[front++]; // get value and incr front
            if (front == maxSize) // deal with wraparound
                front = 0;
            nItems--; // one less item
            return temp;
        }

        public Neighborhood PeekFront() // peek at front of queue
        {
            return queArray[front];
        }

        public bool IsEmpty() // true if queue is empty
        {
            return (nItems == 0);
        }

        public bool IsFull() // true if queue is full
        {
            return (nItems == maxSize);
        }

        public int Size() // number of items in queue
        {
            return nItems;
        }

    }
    class StackX
    {
        private int maxSize; // size of stack array
        private Neighborhood[] stackArray;
        private int top; // top of stack

        public StackX(int s) // constructor
        {
            maxSize = s; // set array size
            stackArray = new Neighborhood[maxSize]; // create array
            top = -1; // no neighbors yet
        }

        public void Push(Neighborhood j) // put neighbor on top of stack
        {
            stackArray[++top] = j; // increment top, insert neighbor
        }


        public Neighborhood Pop() // take neighbor from top of stack
        {
            return stackArray[top--]; // access neighbor, decrement top
        }

        public Neighborhood Peek() // peek at top of stack
        {
            return stackArray[top];
        }

        public bool IsEmpty() // true if stack is empty
        {
            return (top == -1);
        }

        public bool IsFull() // true if stack is full
        {
            return (top == maxSize - 1);
        }

    }
    class Neighborhood
    {
        private string neighborhoodName;
        private List<Delivery> deliveryList;

        /*Encapsulation*/
        public string NeighborhoodName { get => neighborhoodName; set => neighborhoodName = value; } 
        internal List<Delivery> DeliveryList { get => deliveryList; set => deliveryList = value; }

        public Neighborhood(string neighborhoodName, List<Delivery> deliveryList) // constructor
        {
            this.NeighborhoodName = neighborhoodName;
            this.DeliveryList = deliveryList;
        }


    }
    class Delivery
    {
        public string foodName;
        public int number;

        public Delivery(string foodName, int number)// constructor
        {
            this.foodName = foodName;
            this.number = number;
        }
    }
    class Program
    {
        static Random r = new Random(); // object that controls how many food will be delivered

        public static void  printPriorityQ(PriorityQ neighborPriorityQ) // method that printing priority queue structure
        {
            while (!neighborPriorityQ.IsEmpty()) // loop continue until queue is empty
            {

                Neighborhood temp = neighborPriorityQ.Remove(); // removing element from queue  and assign a temp vari

                Console.WriteLine("Neighborhood: " + temp.NeighborhoodName);
                if (temp.DeliveryList.Count == 0) // true if list is empty
                {
                    Console.WriteLine("No delivery at this neighbor");
                    Console.Write(Environment.NewLine);
                }

                foreach (Delivery food in temp.DeliveryList)
                {
                    Console.WriteLine(food.foodName + " " + food.number);

                }
                Console.Write(Environment.NewLine);

            }
        }
        public static void printQueue(Queue neighborQueue)// method that printing queue structure
        {
            while (!neighborQueue.IsEmpty()) // loop continue until queue is empty
            {

                Neighborhood temp = neighborQueue.Remove(); // removing element from queue  and assign a temp vari

                Console.WriteLine("Neighborhood: " + temp.NeighborhoodName);
                if (temp.DeliveryList.Count == 0)
                {
                    Console.WriteLine("No delivery at this neighbor");
                    Console.Write(Environment.NewLine);
                }

                foreach (Delivery food in temp.DeliveryList)
                {
                    Console.WriteLine(food.foodName + " " + food.number);

                }
                Console.Write(Environment.NewLine);

            }
        }
        public static void printStack(StackX neighborStack)// method that printing stack structure
        {
            while (!neighborStack.IsEmpty()) // loop continue until queue is empty
            {

                Neighborhood temp = neighborStack.Pop(); // removing element from stack  and assign a temp vari

                Console.WriteLine("Neighborhood: " + temp.NeighborhoodName);
                if (temp.DeliveryList.Count == 0)
                {
                    Console.WriteLine("No delivery at this neighbor");
                    Console.Write(Environment.NewLine);
                }

                foreach (Delivery food in temp.DeliveryList)
                {
                    Console.WriteLine(food.foodName + " " + food.number);

                }
                Console.Write(Environment.NewLine);

            }

        }

        static void Main(string[] args)
        {
            string[] neighborhoodNameList = { "Özkanlar", "Evka 3", "Atatürk", "Erzene", "Kazımdirik", "Mevlana", "Doğanlar", "Ergene" }; // given neighbor array
            string[] foodList = { "Pizza", "Pilav", "Doner", "Turlu", "Borek", "Kebap", "Sarma", "Dolma", "Baklava", "Corba", "Kizartma",
                "Nohut", "Mantı", "Kofte", "Pilic", "Balık", "Makarna", "Tost", "Simit"}; // food array
            int[] deliveryNumList = { 5, 2, 7, 2, 7, 3, 0, 1 }; // given delivery array

            ArrayList motoCourier = new ArrayList(); //object from ArrayList class

            for (int i = 0; i < neighborhoodNameList.Length; i++)  
            {
                List<Delivery> deliveryList = new List<Delivery>(); // list created with Delivery type
                for (int j = 0; j < deliveryNumList[i]; j++)
                {
                    Delivery newDelivery = new Delivery(foodList[r.Next(0, foodList.Length)], r.Next(1, 100)); //create new Delivert object 
                    deliveryList.Add(newDelivery); // add object to generic list
                }
                Neighborhood myNeighborhood = new Neighborhood(neighborhoodNameList[i], deliveryList); // create new Neighbor object and give delivery list as an argument
                motoCourier.Add(myNeighborhood); // add object to arrayList

            }
            int sumDelivery = 0; 

            // print arraylist
            foreach (Neighborhood neighbor in motoCourier)
            {
                Console.WriteLine("Neighborhood: " + neighbor.NeighborhoodName);
                sumDelivery += neighbor.DeliveryList.Count;

                if (neighbor.DeliveryList.Count == 0)
                {
                    Console.WriteLine("No delivery at this neighbor");
                    Console.Write(Environment.NewLine);
                }

                foreach (Delivery food in neighbor.DeliveryList)
                {
                    Console.WriteLine(food.foodName + " " + food.number);
                }
                Console.Write(Environment.NewLine);
            }
            Console.WriteLine("Sum of Neighbor number: " + motoCourier.Count);
            Console.WriteLine("Sum of delivery number: " + sumDelivery);
            Console.Write(Environment.NewLine);
            Console.WriteLine("**************************************STACK**************************************");

            // create and print stack structure
            StackX neighborStack = new StackX(motoCourier.Count);
            foreach (Neighborhood neighbor in motoCourier)
            {
                neighborStack.Push(neighbor); //add object to stack

            }
            printStack(neighborStack);

            Console.WriteLine("******************************QUEUE******************************");

            // create and print queue structure 
            Queue neighborQueue = new Queue(motoCourier.Count);
            foreach (Neighborhood neighbor in motoCourier)
            {
                neighborQueue.Insert(neighbor); //add object to Queue

            }
            printQueue(neighborQueue);

            Console.WriteLine("******************************PRIORITYQ******************************");
            // create and print stack structure
            PriorityQ neighborPriorityQ = new PriorityQ();
            foreach (Neighborhood neighbor in motoCourier)
            {
                neighborPriorityQ.Add(neighbor); //add object to priorityQ
            }
            printPriorityQ(neighborPriorityQ);







            Console.ReadKey();

        }
    }
}
