using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Klassen
{
    public class Customer
    {
        private int _id;
        private string _fullName;
        private string _adress;
        private DateTime _birthday;
        private DateTime _registrationDate;
        public List<MediaBase> History { get; private set; }

        public Customer() // Konstruktor nur für Placeholder
        {
            _id = -1;
        }
        public Customer(ref LibraryService service,string fullName, string adress, DateTime birthday)
        {
            _id = service.ReturnCustomers().Count + 1;
            _fullName = fullName;
            _adress = adress;
            _birthday = birthday;
            _registrationDate = DateTime.Now;
            History = new List<MediaBase>();
        }
        public int GetId()
        {
            return _id;
        }
        public string[] GetDetails(int Id)
        {
            return new string[] {_fullName, _adress};
        }
        public void PrintHistory()
        {
            Console.WriteLine($"Ausleihhistorie von {this._fullName}:");
            foreach(var item in History)
            {
                if (item.GetMediaType() == MediaType.DVD)
                {
                    Console.Write("Film: ");
                }
                else
                {
                    Console.Write("Buch: ");
                }
                Console.WriteLine($"{item.GetTitle()}");
            }
            Console.WriteLine();
        }
    }
}
