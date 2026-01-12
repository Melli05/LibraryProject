using Klassen;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.CLI
{
    public static class Funktionen // static, damit Klasse nicht instanziert werden muss
    {
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
                uint Laufzeit = uint.Parse(Console.ReadLine()!);

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
            if (Typ == MediaType.DVD)
            {
                foreach (Book B in Service.ReturnBooks())
                {
                    Console.WriteLine($"{B.GetId()} - {B.GetTitle()} von {B.GetAuthor()}");
                }
                Console.Write("Welches Buch soll entfernt werden? ID wählen: ");
                int Input = int.Parse(Console.ReadLine()!);

                Service.Remove(Service.GetSpecificBook(Input)); // Buch wird anhand von ID gesucht und gelöscht
            }
            else
            {
                foreach (Dvd D in Service.ReturnDvds())
                {
                    Console.WriteLine($"{D.GetId()} - {D.GetTitle()} von {D.GetDirector()}");
                }
                Console.Write("Welche DVD soll entfernt werden? ID wählen: ");
                int Input = int.Parse(Console.ReadLine()!);

                Service.Remove(Service.GetSpecificDvd(Input)); // DVD wird anhand von ID gesucht und gelöscht
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
            foreach (Customer K in Service.ReturnCustomers())
            {
                Console.WriteLine($"{K.GetId()} - {K.GetDetails(K.GetId())[0]} Adresse: {K.GetDetails(K.GetId())[1]}");
            }
            Console.Write("Welche Kunde soll entfernt werden?\nID wählen: ");
            int Input = int.Parse(Console.ReadLine()!);

            Service.Remove(Service.GetSpecificCustomer(Input)); // Kunde wird anhand von ID gesucht und gelöscht
        }
        public static void StartALoan(ref LibraryService Service)
        {
            Customer Kunde = new(); // ID = -1
            Book Buch = new(); // ID = -1
            Dvd Disk = new(); // ID = -1

            int EingabeZahl;
            string EingabeString;
            int VorhandeneVerleihe = Service.ReturnLoans().Count;
            bool AuswahlMenü = true; // true, bis Kunde und Buch oder DVD ausgewählt wurden

            while (true)
            {
                if(Kunde.GetId() == -1 || (Buch.GetId() == -1 && Disk.GetId() == -1)) // Wenn Kunden-ID -1 ist UND entweder Buch-ID oder DVD-ID -1 ist (= solange keine gültigen Objekte ausgewählt sind)
                {
                    Console.Clear();
                    Console.WriteLine("Treffen Sie Ihre Auswahl.\n\n" +
                                      "0 - Verleih abbrechen\n" +
                                      "1 - Alle Kunden ausgeben\n" +
                                      "2 - Kunden anhand von ID suchen\n" +
                                      "3 - Alle DVDs ausgeben\n" +
                                      "4 - DVD aufgrund von ID suchen\n" +
                                      "5 - Alle Bücher ausgeben\n" +
                                      "6 - Buch anhand von ID suchen\n");
                }
                else
                {
                    Console.Clear();
                    AuswahlMenü = false;

                    Console.WriteLine("0 - Weiter zum Verleih\n");
                }

                Console.Write("Ihre Auswahl: ");
                EingabeZahl = int.Parse(Console.ReadLine()!);

                if(AuswahlMenü)
                {
                    switch (EingabeZahl)
                    {
                        case 0:
                            Console.Clear();
                            return;

                        case 1:
                            Console.Clear();

                            foreach (Customer K in Service.ReturnCustomers())
                            {
                                Console.WriteLine($"ID: {K.GetId()} - {K.GetDetails(K.GetId())[0]}, {K.GetDetails(K.GetId())[1]}");
                            }

                            Console.Write("KundenID auswählen: ");
                            EingabeZahl = int.Parse(Console.ReadLine()!);
                            Kunde = Service.GetSpecificCustomer(EingabeZahl);

                            if (Kunde == null) // Wenn keine gültige ID eingegeben wurde
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Kunde = new();
                                Console.ReadLine();
                            }

                            break;

                        case 2:
                            Console.Clear();

                            Console.WriteLine("Bitte Id eingeben: ");
                            EingabeZahl = int.Parse(Console.ReadLine()!);
                            Kunde = Service.GetSpecificCustomer(EingabeZahl);

                            if (Kunde == null)
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Gefundener Kunde:\n" +
                                                 $"{Kunde.GetDetails(Kunde.GetId())[0]} {Kunde.GetDetails(Kunde.GetId())[1]}\n");
                            }

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

                            if (Disk == null) // Wenn keine gültige ID eingegeben wurde
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Disk = new();
                                Console.ReadLine();
                            }

                            break;

                        case 4:
                            Console.Clear();

                            Console.WriteLine("Bitte Id eingeben: ");
                            EingabeZahl = int.Parse(Console.ReadLine()!);
                            Disk = Service.GetSpecificDvd(EingabeZahl);

                            if (Disk == null)
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Gefundene DVD:\n" +
                                                 $"{Disk.GetTitle()} ({Disk.GetPubDate()}) von {Disk.GetDirector()}\n");
                            }
                            break;

                        case 5:
                            Console.Clear();

                            foreach (Book B in Service.ReturnBooks())
                            {
                                Console.WriteLine($"{B.GetId()} {B.GetTitle()} von {B.GetAuthor()}");
                            }
                            Console.Write("Buch-ID auswählen: ");
                            EingabeZahl = int.Parse(Console.ReadLine()!);
                            Buch = Service.GetSpecificBook(EingabeZahl);

                            if (Buch == null) // Wenn keine gültige ID eingegeben wurde
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Buch = new();
                                Console.ReadLine();
                            }

                            break;

                        case 6:
                            Console.Clear();

                            Console.WriteLine("Bitte Id eingeben: ");
                            EingabeZahl = int.Parse(Console.ReadLine()!);
                            Buch = Service.GetSpecificBook(EingabeZahl);

                            if (Buch == null)
                            {
                                Console.WriteLine("Enter um zum Menü zurückzukehren.\n");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Gefundenes Buch:\n" +
                                                 $"{Buch.GetTitle()} ({Buch.GetPubDate()}) von {Buch.GetAuthor()}\n");
                            }

                            break;
                    }
                }
                else
                {
                    if (EingabeZahl == 0)
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                Console.Clear();
                if (Kunde.GetId() != -1)
                {
                    Console.WriteLine($"Kunde:\tID: {Kunde.GetId()} - {Kunde.GetDetails(Kunde.GetId())[0]}, {Kunde.GetDetails(Kunde.GetId())[1]}");
                }

                if (Buch.GetId() != -1)
                {
                    Console.WriteLine($"Buch:\tTitel: {Buch.GetTitle()}, von {Buch.GetAuthor()} erschienen am: {Buch.GetPubDate().ToShortDateString()}");

                    Console.WriteLine("Möchten Sie den Verleih abschließen? (Y/N)");
                    EingabeString = Console.ReadLine()!;

                    if (EingabeString == "Y" || EingabeString == "y")
                    {
                        Loan Verleih = new(ref Service, ref Kunde, ref Buch);
                        Service.Add(Verleih);
                        break;
                    }
                    else if (EingabeString == "N" || EingabeString == "n")
                    {
                        break;
                    }
                }

                if (Disk.GetId() != -1)
                {
                    Console.WriteLine($"DVD:\tTitel: {Disk.GetTitle()}, von {Disk.GetDirector()} erschienen am: {Disk.GetPubDate().ToShortDateString()}");

                    Console.Write("Möchten Sie den Verleih abschließen? (Y/N): ");
                    EingabeString = Console.ReadLine()!;

                    if (EingabeString == "Y" || EingabeString == "y")
                    {
                        Loan Verleih = new(ref Service, ref Kunde, ref Disk);
                        Service.Add(Verleih);
                        break;
                    }
                    else if (EingabeString == "N" || EingabeString == "n")
                    {
                        break;
                    }
                }
            }

            if (VorhandeneVerleihe + 1 == Service.ReturnLoans().Count) // Wenn der Count gestiegen ist, war der Verleih erfolgreich
            {
                Console.WriteLine("Verleih erfolgreich abgeschlossen.\nEnter um zum Hauptmenü zurückzukehren.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Verleih abgebrochen.\nEnter um zum Hauptmenü zurückzukehren.");
                Console.ReadLine();
            }
            Console.Clear();
        }
        public static void EndALoan(ref LibraryService Service)
        {
            int EingabeZahl;

            Console.Clear();

            if (Service.ReturnLoans().Where(l => l.GetStatus() == true).Count() == 0) // Wenn keine aktiven Verleihvorgänge vorhanden sind
            {
                Console.WriteLine("Keine offenen Rückgaben.\nEnter um zum Hauptmenü zurückzukehren.\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Welchen Verleih möchten Sie beenden?\n");

                foreach (Loan Verleih in Service.ReturnLoans())
                {
                    if (Verleih.GetStatus())
                    {
                        Verleih.GetInfo();
                    }
                }

                Console.Write("\nAusleih-ID zum zurückgeben auswählen: ");
                EingabeZahl = int.Parse(Console.ReadLine()!);
                Loan AusgesuchterVerleih = Service.GetSpecificLoan(EingabeZahl);
                AusgesuchterVerleih.EndLoan();
            }

            Console.Clear();
        }
    }
}
