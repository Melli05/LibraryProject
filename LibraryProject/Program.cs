using Klassen;

internal class Program
{
    private static void Main(string[] args)
    {
        LibraryService Service = new();

        Customer Test = new(ref Service, "Peter Stöger", "Peterstraße 6, 6020 Pinnsbruck", DateTime.Now);
        Dvd Test1 = new(ref Service, "König der Löwen", "Disney", DateTime.Now,67);
        Service.Add(Test1);
        Dvd Test2 = new(ref Service, "Schweigen der Lämmer", "Disney", DateTime.Now,0,"",3);
        Service.Add(Test2);
        Book Test3 = new(ref Service, "Stefan", "Bettina", DateTime.Now, units:7);
        Service.Add(Test3);
        Book Test4 = new(ref Service, "Bettina", "Stefan", DateTime.Now,"123-55-4322", 240,desc:"Bettina meine Liebe", 10);
        Service.Add(Test4);

        Service.Add(Test);

        foreach (Book book in Service.ReturnBooks())
        {
            book.PrintInfo();
        }
        foreach (Dvd dvd in Service.ReturnDvds())
        {
            dvd.PrintInfo();
        }
        foreach (Customer customer in Service.ReturnCustomers())
        {
            customer.PrintHistory();
            Console.Write($"\n{customer.GetDetails(customer.GetId())[0]},\n{customer.GetDetails(customer.GetId())[1]}\n\n");
        }

        Loan loan = new(ref Service, ref Test, ref Test2);
        Service.Add(loan);
        Loan loan2 = new(ref Service, ref Test, ref Test3);
        Service.Add(loan2);
        Loan loan3 = new(ref Service, ref Test, ref Test1);
        Service.Add(loan3);

        foreach (Book book in Service.ReturnBooks())
        {
            book.PrintInfo();
        }
        foreach (Dvd dvd in Service.ReturnDvds())
        {
            dvd.PrintInfo();
        }
        foreach (Customer customer in Service.ReturnCustomers())
        {
            customer.PrintHistory();
            Console.Write($"\n{customer.GetDetails(customer.GetId())[0]},\n{customer.GetDetails(customer.GetId())[1]}\n\n");
        }

        Loan loan4 = new(ref Service, ref Test, ref Test1);
        Service.Add(loan4);

        foreach (Book book in Service.ReturnBooks())
        {
            book.PrintInfo();
        }
        foreach (Dvd dvd in Service.ReturnDvds())
        {
            dvd.PrintInfo();
        }
        foreach (Customer customer in Service.ReturnCustomers())
        {
            customer.PrintHistory();
            Console.Write($"\n{customer.GetDetails(customer.GetId())[0]},\n{customer.GetDetails(customer.GetId())[1]}\n\n");
        }
        foreach (Loan loansssss in Service.ReturnLoans())
        {
            loansssss.GetInfo();
        }
    }
}