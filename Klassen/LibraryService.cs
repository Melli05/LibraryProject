using System;
using System.Collections.Generic;
using System.Text;

namespace Klassen
{
    public class LibraryService
    {
        private List<Book> _books = null;
        private List<Dvd> _dvds = null;
        private List<Customer> _customers = null;
        private List<Loan> _loans = null;

        public LibraryService()
        {
            _books = new List<Book>();
            _dvds = new List<Dvd>();
            _customers = new List<Customer>();
            _loans = new List<Loan>();
        }
        public void Add(Book book)
        {
            _books.Add(book); 
        }
        public void Add(Dvd dvd)
        {
            _dvds.Add(dvd);
        }
        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }
        public void Add(Loan loan)
        {
            try
            {
                if (loan.GetId() != -1) // ID = -1, wenn Medium nicht mehr verfügbar war
                {
                    _loans.Add(loan);
                }
                else
                {
                    throw new Exception($"Verleih konnte nicht hinzugefügt werden.\n");
                }
            }
            catch(Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
        public List<Book> ReturnBooks()
        {
            try
            {
                if (_books != null)
                {
                    return _books;
                }
                else
                {
                    throw new Exception("Buch-Liste ist null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Dvd> ReturnDvds()
        {
            try
            { 
                if (_dvds != null)
                {
                    return _dvds;
                }
                else
                {
                    throw new Exception("DVD-Liste ist null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Customer> ReturnCustomers()
        {
            try
            {
                if (_customers != null)
                {
                    return _customers;
                }
                else
                {
                    throw new Exception("Kunden-Liste ist null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Loan> ReturnLoans()
        {
            try
            {
                if (_loans != null)
                {
                    return _loans;
                }
                else
                {
                    throw new Exception("Verleih-Liste ist null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Book GetSpecificBook(int id)
        {
            try
            {
                foreach (Book book in _books)
                {
                    if (book.GetId() == id)
                    {
                        return book;
                    }
                }
                throw new Exception($"Kein Buch mit der gesuchten Id: {id} gefunden.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Dvd GetSpecificDvd(int id)
        {
            try
            {
                foreach (Dvd dvd in _dvds)
                {
                    if (dvd.GetId() == id)
                    {
                        return dvd;
                    }
                }
                throw new Exception($"Keine DVD mit der gesuchten Id: {id} gefunden.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Customer GetSpecificCustomer(int id)
        {
            try
            {
                foreach (Customer customer in _customers)
                {
                    if (customer.GetId() == id)
                    {
                        return customer;
                    }
                }
                throw new Exception($"Kein Kunde mit der gesuchten Id: {id} gefunden.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Loan GetSpecificLoan(int id)
        {
            try
            {
                foreach (Loan loan in _loans)
                {
                    if (loan.GetId() == id)
                    {
                        return loan;
                    }
                }
                throw new Exception($"Kein Kunde mit der gesuchten Id: {id} gefunden.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public void Remove(Book book)
        {
            _books.Remove(book);
        }
        public void Remove(Dvd dvd)
        {
            _dvds.Remove(dvd);
        }
        public void Remove(Customer customer)
        {
            _customers.Remove(customer);
        }
        public void Remove(Loan loan)
        {
            _loans.Remove(loan);
        }
    }
}
