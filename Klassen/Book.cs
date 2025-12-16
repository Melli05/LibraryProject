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

        public Book(ref LibraryService service, string title, string author, DateTime pubdate,string isbn = "", uint pages = 0, string desc = "", int units = 0)
        {
            SetId(service.ReturnBooks().Count + 1);           
            SetMediaType(MediaType.Buch);
            SetTitle(title);
            _author = author;
            SetPubDate(pubdate);
            if(pages != 0)
            {
                _pages = pages;
            }
            else if(desc != "")
            {
                SetDescription(desc);
            }
            else if(units != 0)
            {
                SetUnits(units); 
            }
            else if(isbn != "")
            {
                _isbn = isbn;
            }
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
                              $"Veröffentlichung: {GetPubDate()}\n" +
                              $"Seitenanzahl: {GetPages()}\n" +
                              $"Momentan verleihbare Einheiten: {GetUnits()}\n" +
                              $"Beschreibung: {GetDescription()}\n");
        }
    }
}
