using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lab13
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MyNewCollection firstCollection = new MyNewCollection(6, "first");
            MyNewCollection secondCollection = new MyNewCollection(8, "second");

            Journal firstJournal = new Journal();
            firstCollection.CollectionCountChanged += new CollectionHandler(firstJournal.CollectionCountChanged);
            firstCollection.CollectionReferenceChanged += new CollectionHandler(firstJournal.CollectionReferenceChanged);
            Journal secondJournal = new Journal();
            secondCollection.CollectionReferenceChanged += new CollectionHandler(firstJournal.CollectionReferenceChanged);
            secondCollection.CollectionReferenceChanged += new CollectionHandler(secondJournal.CollectionReferenceChanged);

            firstCollection.Add();
            firstCollection.Add();
            firstCollection.Remove(5);
            firstCollection.Remove(4);
            firstCollection.ChangeItem(3, 700);
            secondCollection.Remove(3);
            secondCollection.ChangeItem(6, 300);

            Console.WriteLine("перебор 1ой коллекции");
            foreach (Transport item in firstCollection._collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            firstJournal.Print();
            Console.WriteLine();
            secondJournal.Print();
        }
    }

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : System.EventArgs
    {
        public string NameCollection { get; set; }
        public string ChangeType { get; set; }
        public object Obj { get; set; }

        public CollectionHandlerEventArgs()
        {
            NameCollection = null;
            ChangeType = null;
            Obj = default;
        }

        public CollectionHandlerEventArgs(string nameCollection, string changeType, object obj)
        {
            NameCollection = nameCollection;
            ChangeType = changeType;
            Obj = obj;
        }

        public override string ToString()
        {
            return "Коллекция: " + NameCollection + ", " + ChangeType + " элемент: " + Obj.ToString();
        }
    }
    
    public class MyNewCollection : Collection<Transport>
    {
        public readonly Collection<Transport> _collection;
        private string Name { get; set; }

        public MyNewCollection()
        {
            _collection = null;
            Name = null;
        }

        public MyNewCollection(int size, string name)
        {
            _collection = new Collection<Transport>(size);
            Name = name;
        }

        public new bool Remove(int num)
        {
            Transport item = _collection.Remove(num);

            if (item!=null)
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "элемент удален", item));
                return true;
            }
            else 
            {
                Console.WriteLine("элемент не удален");
                return false;
            }
        }

        public new void Add()
        {
            Transport item=_collection.Add();
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "элемент добавлен", item));
        }

        public void ChangeItem(int number, int speed)
        {
            if (_collection.Change(number, speed)==null)
                return;
            OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "изменение элемента", _collection.Change(number, speed)));
        }
        
        
        public event CollectionHandler CollectionCountChanged;
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }
        
        public event CollectionHandler CollectionReferenceChanged;
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }
    }
    
    public class Journal
    {
        private readonly List<JournalEntry> _journal = new List<JournalEntry>();

        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry entry = new JournalEntry(e.NameCollection, e.ChangeType, e.Obj.ToString());
            _journal.Add(entry);
        }
        
        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry entry = new JournalEntry(e.NameCollection, e.ChangeType, e.Obj.ToString());
            _journal.Add(entry);
        }
        
        public void Print()
        {
            Console.WriteLine("Журнал:");
            foreach (JournalEntry item in _journal)
                Console.WriteLine(item);
        }
    }
    
    public class JournalEntry
    {
        public string NameCollection { get; set; }
        public string ChangeType { get; set; }
        public Object Obj { get; set; }

        public JournalEntry()
        {
            NameCollection = null;
            ChangeType = null;
            Obj = default;
        }

        public JournalEntry(string name, string changeType, object o)
        {
            NameCollection = name;
            ChangeType = changeType;
            Obj = o;
        }

        public override string ToString()
        {
            return NameCollection + ": " + ChangeType +" - " + Obj.ToString();
        }
    }
}
