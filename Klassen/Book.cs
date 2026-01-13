using System;
using System.Collections.Generic;
using System.Text;

namespace Klassen
{
    public class Book : MediaBase
    {
        private string _author;
        private uint _pages = 250;
        private string _isbn = "";

        public Book() // Placeholder Konstruktor
        {
            SetId(-1);
        }
        public Book(ref LibraryService service, string title, string author, DateTime pubdate, string isbn = "", uint pages = 0, string desc = "", int units = 0)
        {
            SetId(service.ReturnBooks().Count + 1);
            SetMediaType(MediaType.Buch);
            SetTitle(title);
            _author = author;
            SetPubDate(pubdate);
            if (pages != 0)
            {
                _pages = pages;
            }
            if (desc != "")
            {
                SetDescription(desc);
            }
            if (units != 0)
            {
                SetUnits(units);
            }
            if (isbn != "")
            {
                _isbn = isbn;
            }
        }
        public override string GetDescription()
        {
            return $"Die Beschreibung des Buchs lautet wie folgt:\n{base.GetDescription()}";
        }
        public string GetAuthor()
        {
            return _author;
        }

        public uint GetPages()
        {
            return _pages; 
        }
        public string GetIsbn()
        {
            return _isbn; 
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Buch-ID: {GetId()}\n" +
                              $"Titel: {GetTitle()}\n" +
                              $"Autor: {GetAuthor()}\n" +
                              $"ISBN: {this._isbn}\n" +
                              $"Veröffentlichung: {GetPubDate().ToShortDateString()}\n" +
                              $"Seitenanzahl: {GetPages()}\n" +
                              $"Momentan verleihbare Einheiten: {GetUnits()}\n" +
                              $"{this.GetDescription()}\n");
        }
    }
}
