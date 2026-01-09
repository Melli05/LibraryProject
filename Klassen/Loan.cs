using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Klassen
{
    public class Loan
    {
        private int _id = -1;
        private Customer _customer = null;
        private MediaBase _media = null;
        private DateTime _startDate = DateTime.MaxValue;
        private DateTime _endDate = DateTime.MaxValue;
        private DateTime _dueDate = DateTime.MaxValue;
        private bool _isActive = true;

        public Loan(ref LibraryService service, ref Customer customer, ref Book book)
        {
            try
            {
                if (book.GetUnits() - 1 >= 0)
                {
                    _id = service.ReturnLoans().Count + 1; 
                    _customer = customer;
                    _media = book;
                    _startDate = DateTime.Now;
                    _dueDate = _startDate.AddDays(21); // 3 Wochen Zeit um ein Buch zu lesen
                    book.SetUnits(book.GetUnits() - 1);
                    customer.History.Add(book);
                }
                else
                {
                    _isActive = false;
                    throw new Exception("Buch kann nicht verliehenw werden, keine Exemplare verfügbar.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Loan(ref LibraryService service, ref Customer customer, ref Dvd dvd)
        {
            try
            {
                if (dvd.GetUnits() - 1 >= 0)
                {
                    _id = service.ReturnLoans().Count + 1;
                    _customer = customer;
                    _media = dvd;
                    _startDate = DateTime.Now;
                    _dueDate = _startDate.AddDays(7); // 1 Woche Zeit um einen Film zu schauen
                    dvd.SetUnits(dvd.GetUnits() - 1);
                    customer.History.Add(dvd);
                }
                else
                {
                    _isActive = false;
                    throw new Exception("DVD kann nicht verliehen werden, keine Exemplare verfügbar.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetId()
        {
            return _id; 
        }
        public void SetId(int id)
        {
            try
            {
                if (id > 0)
                {
                    this._id = id;
                }
                else
                {
                    throw new Exception("Verleih-ID kann nicht kleiner oder gleich 0 sein.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public bool GetStatus()
        {
            return _isActive; 
        }
        public void GetInfo()
        {
            try
            {
                if (_customer != null && _media != null)
                {
                    string infotext = $"Information zu Verleih {this._id}:\n" +
                                      $"Kunde: {this._customer.GetDetails(this._customer.GetId())[0]}, {this._customer.GetDetails(this._customer.GetId())[1]}\n" +
                                      $"verliehenes Medium: {this._media.GetMediaType()} - {this._media.GetTitle()}\n" +
                                      $"ausgeliehen am: {this._startDate.ToShortDateString()}\n";
                    if (this._endDate != DateTime.MaxValue)
                    {
                        infotext += $"zurückgegeben am: {this._endDate}\n";
                    }
                    else
                    {
                        infotext += "Rückgabe ausstehend.";
                    }

                    Console.WriteLine(infotext);
                }
                else
                {
                    throw new Exception($"Info zum Verleih kann nicht ausgegeben werden: Kunde: {this._customer}, Medium: {this._media}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void EndLoan()
        {
            this._endDate = DateTime.Now;
            this._media.SetUnits(this._media.GetUnits() + 1);
            _isActive = false;
        }
    }
}
