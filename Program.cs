using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3._1
{
    class Node
    {   //sahalar
        public Neighborhood data; // data used as key value
        public Node leftChild; // this node’s left child
        public Node rightChild; // this node’s right child
        public void displayNode()
        {
            Console.WriteLine("**************Neighbor**************");
            Console.Write(data.NeighborhoodName + ": ");
            Console.Write(Environment.NewLine);
            foreach (List<Food> item in data.FoodList) //yemek bilgisine erişmek için iç içe for döngüsü
            {
                Console.WriteLine("Siparişler:");
                foreach (Food item2 in item)
                {

                    Console.WriteLine("Food: " + item2.foodName);
                    Console.WriteLine("Amount: " + item2.number);
                    Console.WriteLine("Unit Price: " + item2.price);
                    Console.Write(Environment.NewLine);
                }
                Console.Write(Environment.NewLine); Console.Write(Environment.NewLine);
            }
        }
    } //end of Node class


    class Tree
    {   //sahalar
        private Node root; // the only data field in Tree
        public int nodeNumber;
        public Tree()
        {
            root = null;
            nodeNumber = 0;
        }

        public Node getRoot()
        { return root; }
        public int MaxDepth(Node root)
        {
            if (root == null)
                return -1;
            else
            {
                /* compute the depth of each subtree */
                int leftDepth = MaxDepth(root.leftChild);
                int rightDepth = MaxDepth(root.rightChild);

                /* use the larger one */
                if (rightDepth > leftDepth)
                    return (rightDepth + 1);
                else
                    return (leftDepth + 1);
            }
        }
        public Node find(string key)
        {
            Node current = root; // start at root

            while (current.data.NeighborhoodName != key) // while no match,
            {
                if (current.data.NeighborhoodName.CompareTo(key) > 0)
                { // go left?
                    current = current.leftChild;
                }
                else
                {
                    current = current.rightChild; // or go right?
                }
                if (current == null)
                { // if no child,
                    return null; // didn’t find it
                }
            }
            return current; // found it
        }


        public void Insert(Neighborhood newData)
        {
            nodeNumber++;
            Node newNode = new Node();
            newNode.data = newData;
            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                Node parent;

                while (true)
                {
                    parent = current;
                    if (current.data.NeighborhoodName.CompareTo(newData.NeighborhoodName) > 0)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                } // end while


            }
        }

        public void InOrder(Node localRoot)
        {
            if (localRoot != null)
            {
                InOrder(localRoot.leftChild);
                localRoot.displayNode();
                InOrder(localRoot.rightChild);
            }
        }
        public void DiscountFood(string key, Node localRoot, ref int a) // ismi verilen yemeğin kaç adet olduğunu yazdıran ve indirim yapan metod
        {
            Node parent = localRoot;

            if (localRoot != null)
            {
                DiscountFood(key, localRoot.leftChild, ref a);
                foreach (List<Food> item in localRoot.data.FoodList) //yemek bilgisine erişmek için iç içe for döngüsü
                {
                    foreach (Food item2 in item)
                    {
                        if (key == item2.foodName)
                        {
                            a += item2.number; //toplam adet hesabı
                            item2.price = (int)(item2.price * 0.9); // birim fiyatta indirim uygulandı
                        }

                    }

                }
                DiscountFood(key, localRoot.rightChild, ref a);
            }



        }


        public void SpesificDisplayTree(string name) //ismi verilen mahallenin 150 tl üzeri siparişlerini yazdıran metod
        {
            Node neighbor = find(name); //find metodu ile aranan mahalle bulunur
            Console.WriteLine("The searched neighborhood: " + neighbor.data.NeighborhoodName);
            foreach (List<Food> item in neighbor.data.FoodList) // yiyecek bilgisine erişmek için iç içe foreach döngüsü
            {
                int totalPrice = 0; // siparişin 150 tl üzeri olduğunu belirlemek için değişken
                foreach (Food food in item)
                {
                    totalPrice += food.number * food.price; // sipariş tutarı hesabı
                }
                if (totalPrice > 150) // tutar 150den büyükse true
                {
                    Console.Write(Environment.NewLine);
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Siparişler:");
                    foreach (Food food in item)
                    {
                        Console.WriteLine("Food: " + food.foodName);
                        Console.WriteLine("Amount: " + food.number);
                        Console.WriteLine("Unit Price: " + food.price);
                        Console.Write(Environment.NewLine);
                    }

                }
            }
        }


    }// end class Tre




    class Food
    {
        //sahalar
        public string foodName;
        public int number;
        public int price;



        public Food(string foodName, int number, int price)// constructor
        {
            this.foodName = foodName;
            this.number = number;
            this.price = price;
        }
    }
    class Neighborhood
    {
        private string neighborhoodName;
        private List<List<Food>> foodList;

        /*Encapsulation*/
        public string NeighborhoodName { get => neighborhoodName; set => neighborhoodName = value; }
        internal List<List<Food>> FoodList { get => foodList; set => foodList = value; }

        public Neighborhood(string neighborhoodName, List<List<Food>> foodList) // constructor
        {
            this.NeighborhoodName = neighborhoodName;
            this.foodList = foodList;
        }


    }
    class Program
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            Tree tree = new Tree();
            string[] neighborArr = { "Evka 3", "Özkanlar", "Atatürk", "Erzene", "Kazımdirik" };
            string[] foodArr = { "Türlü 20", "Mantı 25", "Kızartma 10", "Kebap 50", "Pilav 5", "Döner 20", "İskender 30", "Bira 20", "Kola 10", "Ayran 5" };


            for (int i = 0; i < neighborArr.Length; i++)
            {
                List<List<Food>> list1 = new List<List<Food>>(); //sipariş listesi
                for (int k = 0; k < r.Next(5, 11); k++) //5-10 arasında rastgele sayıda  sipariş listesi oluşturmak için döngü
                {
                    List<Food> foodList = new List<Food>(); //yemek listesi
                    ArrayList previousFoods = new ArrayList(); // holds  previous random foods.
                    for (int j = 0; j < r.Next(3, 6); j++)//3-5 arasında rastgele yiyecek/içecek  üretmek için döngü
                    {
                        int random = 0;
                        do
                        {
                            random = r.Next(0, foodArr.Length);
                        }
                        while (previousFoods.Contains(random));
                        previousFoods.Add(random);

                        string foodtmp = foodArr[random]; // foodArr den rastgele çekilen yemek
                        string[] foodtmpArr = foodtmp.Split(' '); // fiyatını ve ismini ayırmak için split metodu
                        Food food = new Food(foodtmpArr[0], r.Next(1, 9), Int16.Parse(foodtmpArr[1])); // parse metodu ile integer dönüşümü
                        foodList.Add(food);// yemek listeye eklendi
                    }
                    list1.Add(foodList); // yemek listesi sipariş listesine eklendi
                }
                Neighborhood neighbor = new Neighborhood(neighborArr[i], list1);
                tree.Insert(neighbor);

            }
            tree.InOrder(tree.getRoot());  // ağaç inOrder şeklinde dolaşılarak yazdırıldı
            int depth = tree.MaxDepth(tree.getRoot()); // ağacın derinliği hesaplandı
            Console.WriteLine("Depth of tree: " + depth);
            Console.Write(Environment.NewLine);
            Console.WriteLine("*********************************************************************************************************************");
            Console.WriteLine("Information on orders over 150 TL in a named neighborhood: ");
            tree.SpesificDisplayTree("Evka 3"); // mahalle ismi değiştirilerek başka çıktılar test edilebilir
            Console.WriteLine("*********************************************************************************************************************");
            int totalAmount = 0;
            string wantedFood = "Kebap"; // değişken değiştirilerek farklı çıktılar üretilebilir.
            tree.DiscountFood(wantedFood, tree.getRoot(), ref totalAmount);
            Console.WriteLine("Total number of "+ wantedFood+ ": " + totalAmount);
            Console.Write(Environment.NewLine); Console.Write(Environment.NewLine);
            Console.WriteLine("**********************************************Tree After Discount**********************************************");
            Console.Write(Environment.NewLine);
            tree.InOrder(tree.getRoot());




            Console.ReadKey();
        }
    }
}
