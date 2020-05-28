using System;
using System.Collections;
using System.Collections.Generic;
using static lab13.Transport;

namespace lab13
{
    public class MyCollection<T> : IEnumerable
    {
        private int Length = 0;
        public Transport First;

        public MyCollection()
        {
            Length = 0;
            First = null;
        } //пустой конструктор

        public MyCollection(int size)
        {
            First = new Transport();
            First = First.MakeList(size);
            Length = size;
        } //

        public MyCollection(MyCollection<Transport> c)
        {
            Length = c.Length;
            First = c.First;
        }

        public void Add(Transport item, int number)
        {
            if (item == null) return;

            Length++;

            if (First == null)
            {
                First = item;
                return;
            }

            if (number == 1)
            {
                item.next = First;
                First = item;
                return;
            }

            Transport transport = First;
            for (int i = 1; i < number - 1 && transport != null; i++)
                transport = transport.next;
            if (transport == null)
            {
                Console.WriteLine("error. размер листа меньше выбранного номера");
                return;
            }

            item.next = transport.next;
            transport.next = item; //добавление нового элемента
        }

        public void Add(List<Transport> items, int number)
        {
            foreach (Transport item in items)
            {
                Add(item, number);
                number++;
            }
        }

        public void Delete(Transport transport)
        {
            Length--;
            First.Delete(First, transport);
        }

        public Transport SearchByValue(int maxspeed)
        {
            int count = 1;
            foreach (Transport item in this)
            {
                if (item.MaxSpeed == maxspeed)
                {
                    Console.WriteLine("Элемент под номером " + count);
                    return item;
                }

                count++;
            }

            Console.WriteLine("Элемента нет в коллекции");
            return null;
        }

        public MyCollection<Transport> Copy()
        {
            MyCollection<Transport> tmp = new MyCollection<Transport>();
            tmp.Length = Length;
            tmp.First = First;
            return tmp;
        }

        public MyCollection<Transport> Clone()
        {
            MyCollection<Transport> tmp = new MyCollection<Transport>();
            tmp.Length = Length;
            int number = 1;
            foreach (Transport item in this)
            {
                Transport tr = (Transport)item.Clone();
                tmp.Add(tr, number);
                number++;
            }

            return tmp;
        }

        public IEnumerator GetEnumerator()
        {
            return  GetEnumerator();
        }

        public void Print()
        {
            First.ShowList(First);
        }
    }
}