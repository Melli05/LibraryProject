using Klassen;

internal class Program
{
    private static void Main(string[] args)
    {
        LibraryService Service = new();

        while (true)
        {
            try
            {
                Console.WriteLine("Was möchten Sie tun?\n" +
                                  "0 - Programm beenden\n" +
                                  "1 - Medium hinzufügen\n" +
                                  "2 - Medium ausleihen\n" +
                                  "3 - Medium zurückgeben\n" +
                                  "4 - Medium entfernen\n" +
                                  "5 - Kunden hinzufügen\n" +
                                  "6 - Kunden entfernen\n");
                Console.Write("Ihre Auswahl: ");
                int Eingabe = int.Parse(Console.ReadLine()!);
                Console.Clear();

                switch (Eingabe)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Was möchten Sie hinzufügen?\n" +
                                          "1 - DVD\n" +
                                          "2 - Buch\n");
                        Console.Write("Ihre Auswahl: ");
                        Eingabe = int.Parse(Console.ReadLine()!);
                        switch (Eingabe)
                        {
                            case 1:
                                AddMedia(MediaType.DVD, ref Service);
                                break;
                            case 2:
                                AddMedia(MediaType.Buch, ref Service);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Keine gültige Auswahl für das Medium getroffen.\nZurück im Hauptmenü.\n");
                                break;
                        }
                        break;
                    case 2:
                        StartALoan(ref Service);
                        break;
                    case 3:
                        EndALoan(ref Service);
                        break;
                    case 4:
                        Console.WriteLine("Was möchten Sie entfernen?\n" +
                                          "1 - DVD\n" +
                                          "2 - Buch\n");
                        Console.Write("Ihre Auswahl: ");
                        Eingabe = int.Parse(Console.ReadLine()!);
                        switch (Eingabe)
                        {
                            case 1:
                                RemoveMedia(ref Service, MediaType.DVD);
                                break;
                            case 2:
                                RemoveMedia(ref Service, MediaType.Buch);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Keine gültige Auswahl für das Medium getroffen.\nZurück im Hauptmenü.\n");
                                break;
                        }
                        break;
                    case 5:
                        AddCustomer(ref Service);
                        break;
                    case 6:
                        RemoveCustomer(ref Service);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Keine gültige Auswahl getroffen!\n");
                        break;
                }
            }
            catch(FormatException)
            {
                Console.Clear();
                Console.WriteLine("Es wurde keine (gültige) Zahl in die Eingabe eingetippt.\n");
            }
            catch(OverflowException)
            {
                Console.Clear();
                Console.WriteLine("Ein so großer Wert wurde hier nicht gefragt...\n");
            }
        }
    }

    public static void AddMedia(MediaType Typ, ref LibraryService Service)
    {
        Console.Write("Titel: ");
        string Titel = Console.ReadLine()!;
        Console.Write("Beschreibung (optional): ");
        string Desc = Console.ReadLine()!;
        Console.Write("Erscheinungsdatum: ");
        DateTime PubDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Verfügbare Einheiten: ");
        int Units = int.Parse(Console.ReadLine()!);

        if (Typ == MediaType.DVD)
        {
            Console.Write("Regisseur: ");
            string Regie = Console.ReadLine()!;
            Console.Write("Länge des Films in Minuten: ");
            uint Laufzeit = uint.Parse(Console.ReadLine());
            
            Dvd Disk = new(ref Service, Titel, Regie, PubDate, Laufzeit, Desc, Units);

            Service.Add(Disk);
        }
        else
        {
            Console.Write("Autor: ");
            string Autor = Console.ReadLine()!;
            Console.Write("Seitenanzahl: ");
            uint Seiten = uint.Parse(Console.ReadLine()!);
            Console.Write("ISBN: ");
            string Isbn = Console.ReadLine()!;

            Book Buch = new(ref Service, Titel, Autor, PubDate, Isbn, Seiten, Desc, Units);

            Service.Add(Buch);
        }

        Console.Clear();
        Console.WriteLine("Medium erfolgreich hinzugefügt.\n");
    }
    public static void RemoveMedia(ref LibraryService Service, MediaType Typ)
    {
        if(Typ == MediaType.DVD)
        {
            foreach (Book B in Service.ReturnBooks())
            {
                Console.WriteLine($"{B.GetId()} - {B.GetTitle()} von {B.GetAuthor()}");
            }
            Console.Write("Welches Buch soll entfernt werden? ID wählen: ");
            int Input = int.Parse(Console.ReadLine()!);

            Service.Remove(Service.GetSpecificBook(Input));
        }
        else
        {
            foreach (Dvd D in Service.ReturnDvds())
            {
                Console.WriteLine($"{D.GetId()} - {D.GetTitle()} von {D.GetDirector()}");
            }
            Console.Write("Welche DVD soll entfernt werden? ID wählen: ");
            int Input = int.Parse(Console.ReadLine()!);

            Service.Remove(Service.GetSpecificDvd(Input));
        }
    }
    public static void AddCustomer(ref LibraryService Service)
    {
        Console.Write("Vor- und Nachname: ");
        string Name = Console.ReadLine()!;
        Console.Write("Straße und Hausnummer: ");
        string Adresse = Console.ReadLine()!;
        Console.Write("PLZ und Ort: ");
        Adresse += " " + Console.ReadLine();
        Console.Write("Geburtsdatum: ");
        DateTime Geburtsdatum = DateTime.Parse(Console.ReadLine()!);

        Customer Kunde = new(ref Service, Name, Adresse, Geburtsdatum);

        Service.Add(Kunde);

        Console.Clear();
        Console.WriteLine("Kunde erfolgreich hinzugefügt.");
    }
    public static void RemoveCustomer(ref LibraryService Service)
    {
        foreach(Customer K in Service.ReturnCustomers())
        {
            Console.WriteLine($"{K.GetId()} - {K.GetDetails(K.GetId())[0]} Adresse: {K.GetDetails(K.GetId())[1]}");
        }
        Console.Write("Welche Kunde soll entfernt werden? ID wählen: ");
        int Input = int.Parse(Console.ReadLine()!);

        Service.Remove(Service.GetSpecificCustomer(Input));
    }
    public static void StartALoan(ref LibraryService Service)
    {
        int EingabeZahl;
        string EingabeString;

        Customer Kunde = new();
        Book Buch = new();
        Dvd Disk = new();

        while(true)
        {
            Console.WriteLine("Treffen Sie ihre Auswahl.\n\n" +
                              "0 - Weiter zum Verleih\n" +
                              "1 - Alle Kunden ausgeben\n" +
                              "2 - Kunden anhand von ID suchen\n" +
                              "3 - Alle DVDs ausgeben\n" +
                              "4 - DVD aufgrund von ID suchen\n" +
                              "5 - Alle Bücher ausgeben\n" +
                              "6 - Buch anhand von ID suchen\n");
            Console.Write("Ihre Auswahl: ");

            EingabeZahl = int.Parse(Console.ReadLine()!);

            switch(EingabeZahl)
            {
                case 0:
                    break;
                case 1:
                    Console.Clear();

                    foreach(Customer K in Service.ReturnCustomers())
                    {
                        Console.WriteLine($"{K.GetId()} {K.GetDetails(K.GetId())[0]}, {K.GetDetails(K.GetId())[1]}");
                    }
                    Console.Write("KundenID auswählen: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Kunde = Service.GetSpecificCustomer(EingabeZahl);

                    break;
                case 2:
                    Console.Clear();

                    Console.WriteLine("Bitte Id eingeben: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Kunde = Service.GetSpecificCustomer(EingabeZahl);

                    Console.WriteLine("Gefundener Kunde:\n" +
                                     $"{Kunde.GetDetails(Kunde.GetId())[0]} {Kunde.GetDetails(Kunde.GetId())[1]}\n"); 

                    break;
                case 3:
                    Console.Clear();

                    foreach (Dvd D in Service.ReturnDvds())
                    {
                        Console.WriteLine($"{D.GetId()} {D.GetTitle()} von {D.GetDirector()}");
                    }
                    Console.Write("DVD-ID auswählen: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Disk = Service.GetSpecificDvd(EingabeZahl);

                    break;
                case 4:
                    Console.Clear();

                    Console.WriteLine("Bitte Id eingeben: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Disk = Service.GetSpecificDvd(EingabeZahl);

                    Console.WriteLine("Gefundene DVD:\n" +
                                     $"{Disk.GetTitle()} ({Disk.GetPubDate()}) von {Disk.GetDirector()}\n");

                    break;
                case 5:
                    Console.Clear();

                    foreach (Book B in Service.ReturnBooks())
                    {
                        Console.WriteLine($"{B.GetId()} {B.GetTitle()} von {B.GetAuthor()}");
                    }
                    Console.Write("DVD-ID auswählen: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Buch = Service.GetSpecificBook(EingabeZahl);

                    break;
                case 6:
                    Console.Clear();

                    Console.WriteLine("Bitte Id eingeben: ");
                    EingabeZahl = int.Parse(Console.ReadLine()!);
                    Buch = Service.GetSpecificBook(EingabeZahl);

                    Console.WriteLine("Gefundene DVD:\n" +
                                     $"{Buch.GetTitle()} ({Buch.GetPubDate()}) von {Buch.GetAuthor()}\n");

                    break;
            }
            if(EingabeZahl == 0)
            {
                break;
            }
        }

        while(true)
        {
            Console.Clear();
            if (Kunde.GetId() != -1)
            {
                Console.WriteLine($"Kunde: ({Kunde.GetId()}) {Kunde.GetDetails(Kunde.GetId())[0]}, {Kunde.GetDetails(Kunde.GetId())[1]}");
            }

            if (Buch.GetId() != -1)
            {
                Console.WriteLine($"Buch:\nTitel: {Buch.GetTitle()}, von {Buch.GetAuthor()} erschienen am: {Buch.GetPubDate()}");

                Console.WriteLine("Möchten Sie den Verleih abschließen? (Y/N)");
                EingabeString = Console.ReadLine()!;

                if (EingabeString == "Y" || EingabeString == "y")
                {
                    Loan Verleih = new(ref Service, ref Kunde, ref Buch);
                    Service.Add(Verleih);
                    break;
                }
            }

            if (Disk.GetId() != -1)
            {
                Console.WriteLine($"DVD:\nTitel: {Disk.GetTitle()}, von {Disk.GetDirector()} erschienen am: {Disk.GetPubDate}");

                Console.WriteLine("Möchten Sie den Verleih abschließen? (Y/N)");
                EingabeString = Console.ReadLine()!;

                if (EingabeString == "Y" || EingabeString == "y")
                {
                    Loan Verleih = new(ref Service, ref Kunde, ref Disk);
                    Service.Add(Verleih);
                    break;
                }
            }
        }
        
        Console.WriteLine("Verleih erfolgreich abgeschlossen.\n");
    }
    public static void EndALoan(ref LibraryService Service)
    {
        int EingabeZahl;

        Console.Clear();

        Console.WriteLine("Welchen Verleih möchten Sie beenden?");
        foreach(Loan Verleih in Service.ReturnLoans())
        {
            if(Verleih.GetStatus())
            {
                Verleih.GetInfo();
            }
        }

        EingabeZahl = int.Parse(Console.ReadLine()!);
        Loan AusgesuchterVerleih = Service.GetSpecificLoan(EingabeZahl);
        AusgesuchterVerleih.EndLoan();
    }
}