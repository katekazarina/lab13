using System;
using System.Threading;

namespace lab13
{
    public class Transport //однонаправленный список
    {
        public int MaxSpeed { get; set; }
        public Transport Next;

        public Transport()
        {
            Thread.Sleep(50);
            Random rnd = new Random();
            MaxSpeed = rnd.Next(100, 401);
            Next = null;
        }

        public Transport(int speed)
        {
            MaxSpeed = speed;
            Next = null;
        }

        public Transport Remove(Transport first, int number)
        {
            if (first == null)
            {
                Console.WriteLine("error. Список пуст");
                return null;
            }

            if (number == 1)
            {
                first = first.Next;
                return first;
            }

            Transport transport = first;
            //поиск элемента для удаления и выбор предыдущего
            for (int i = 1; i < number - 1 && transport != null; i++)
            {
                transport = transport.Next;
            }

            if (transport == null || transport.Next == null)
            {
                Console.WriteLine("error. размер листа меньше выбранного номера");
                return null;
            }

            Transport item = transport.Next;
            transport.Next = transport.Next.Next; //удаление элемента
            return item;
        } //удаление элемента по номеру, возвращает удаляемый элемент

        public Transport Change(Transport first, int number, int speed)
        {
            Transport transport = first;
            int i = 0;
            
            do
            {
                i++;
                transport = transport.Next;
            } while (i!=number);

            transport.MaxSpeed = speed;
            return transport;
        }
        
        public override string ToString()
        {
            return "Transport: max speed - " + MaxSpeed;
        }

        public Transport MakeTransport()
        {
            Transport t = new Transport();
            return t;
        } //создание элемента списка

        public Transport MakeList(int size)
        {
            Transport first = MakeTransport(); //первый элемент
            for (int i = 1; i < size; i++)
            {
                Transport t = MakeTransport(); //создание элемента и добавление в начало списка
                t.Next = first;
                first = t;
            }

            return first;
        } //добавление в начало

        public void ShowList(Transport first)
        {
            if (first == null)
            {
                Console.WriteLine("лист пуст");
                return;
            }

            Transport transport = first;
            while (transport != null)
            {
                Console.WriteLine(transport.ToString());
                transport = transport.Next;
            }

            Console.WriteLine();
        } //печать
    }
}
