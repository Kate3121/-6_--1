using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лр6_Пр_1
{
    class TrafficLight
    {
        //делегат, який відповідає всім оброблювачам.
        public delegate void Hadler();
        //опис подій СТАТИЧНИХ.
        public static event Hadler Event1; //подія, горітиме зелений.
        public static event Hadler Event2;// подія, горітиме червоний.
        public void ChangeLight(int i)
        {
            if (i % 2 == 0)// тут буде подія, горітиме червоний.
            { Event2(); }//генерує події, горітиме червоний
            else //тут буде подія, горітиме зелений.
            { Event1(); } //генерує події,горітиме зелений
        }
        public void Red()//обробник події, горить червоний.
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Горить червоний");
        }
        public void Green()//обробник події, горить зелений.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Горить зелений");
        }
    }
    class Pedestrian
    {
        string name;
        public Pedestrian() { }
        public Pedestrian(string name)
        {
            this.name = name;
        }
        public void Go() //обробник події, горить червоний
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} iду", name);
            //відписуємось від події
            //Ліворуч від = клас та подія, праворуч об'єкт this та метод
            TrafficLight.Event1 -= this.Stand;
            TrafficLight.Event2 -= this.Go;
        }
        public void Stand()//обробник події, горить зелений
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} чекаю", name);
        }
    }
    class Driver
    {
        string name;
        public Driver() { }
        public Driver(string name)
        {
            this.name = name;
        }
        public void Ride()//обробник події, горить червоний.
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} Їду", name);
            //відписуємось від події
            //Ліворуч від = клас та подія, праворуч об'єкт this та метод
            TrafficLight.Event1 -= this.Ride;
            TrafficLight.Event2 -= this.Stand;
        }
        public void Stand()//обробник події, горить зелений.
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} чекаю", name);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            //об'єкт світлофор
            TrafficLight svet = new TrafficLight();
            //передплата об'єкта svet на подію Event1
            //Обробник події називається Green
            TrafficLight.Event1 += svet.Green;
            //передплата об'єкта svet на подію Event2
            //Обробник події називається Red
            TrafficLight.Event2 += svet.Red;
            for (int i = 1; i <= 5; i++)
            {
                //об'єкт i-й пішохід
                Pedestrian pesh = new Pedestrian("Пiшохiд " + i);
                //об'єкт i-й водiй
                Driver vod = new Driver("Водiй " + i);
                //передплата пішохода та водія на 1 подію
                TrafficLight.Event1 += pesh.Stand;
                TrafficLight.Event1 += vod.Ride;
                //передплата пішохода та водія на 2 подію
                TrafficLight.Event2 += pesh.Go;
                TrafficLight.Event2 += vod.Stand;
                svet.ChangeLight(i);
            }
            Console.ReadKey();
        }
    }
}

    
