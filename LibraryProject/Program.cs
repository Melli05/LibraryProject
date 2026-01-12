using Klassen;

namespace LibraryProject.CLI
{
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
                                      "1 - Medien bearbeiten\n" +
                                      "2 - Kunden bearbeiten\n" +
                                      "3 - Daten anzeigen\n");
                    Console.Write("Ihre Auswahl: ");
                    int Eingabe = int.Parse(Console.ReadLine()!);
                    Console.Clear();

                    switch (Eingabe)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;

                        case 1:
                            Console.WriteLine("0 - Zurück zum Hauptmenü\n" +
                                              "1 - Medium hinzufügen\n" +
                                              "2 - Medium ausleihen\n" +
                                              "3 - Medium zurückgeben\n" +
                                              "4 - Medium entfernen\n");
                            Console.Write("Ihre Auswahl: ");
                            Eingabe = int.Parse(Console.ReadLine()!);

                            switch(Eingabe)
                            {
                                case 0:
                                    Console.Clear();
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
                                            Console.Clear();
                                            Funktionen.AddMedia(MediaType.DVD, ref Service);
                                            break;
                                        case 2:
                                            Console.Clear();
                                            Funktionen.AddMedia(MediaType.Buch, ref Service);
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Keine gültige Auswahl für das Medium getroffen.\nZurück im Hauptmenü.\n");
                                            break;
                                    }
                                    break;

                                case 2:
                                    Console.Clear();
                                    Funktionen.StartALoan(ref Service);
                                    break;

                                case 3:
                                    Console.Clear();
                                    Funktionen.EndALoan(ref Service);
                                    break;

                                case 4:
                                    Console.Clear();
                                    Console.WriteLine("Was möchten Sie entfernen?\n" +
                                                      "1 - DVD\n" +
                                                      "2 - Buch\n");
                                    Console.Write("Ihre Auswahl: ");
                                    Eingabe = int.Parse(Console.ReadLine()!);

                                    switch (Eingabe)
                                    {
                                        case 1:
                                            Console.Clear();
                                            Funktionen.RemoveMedia(ref Service, MediaType.DVD);
                                            break;
                                        case 2:
                                            Console.Clear();
                                            Funktionen.RemoveMedia(ref Service, MediaType.Buch);
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Keine gültige Auswahl für das Medium getroffen.\nZurück im Hauptmenü.\n");
                                            break;
                                    }
                                    break;
                            }
                            break;

                        case 2:
                            Console.Write("Was möchten Sie tun?\n" +
                                          "0 - Zurück zum Hauptmenü\n" +
                                          "1 - Kunden hinzufügen\n" +
                                          "2 - Kunden entfernen\n");
                            Console.Write("Ihre Auswahl:");
                            Eingabe = int.Parse(Console.ReadLine()!);

                            switch(Eingabe)
                            {
                                case 0:
                                    Console.Clear();
                                    break;
                                case 1:
                                    Console.Clear();
                                    Funktionen.AddCustomer(ref Service);
                                    break;
                                case 2:
                                    Console.Clear();
                                    Funktionen.RemoveCustomer(ref Service);
                                    break;
                            }
                            break;

                        case 3:
                            Console.Write("Was möchten Sie tun?\n" +
                                          "0 - Zurück zum Hauptmenü\n" +
                                          "1 - Medien anzeigen\n" +
                                          "2 - Kunden anzeigen\n" +
                                          "3 - Verleihvorgänge anzeigen\n");

                            Console.Write("Ihre Auswahl:");
                            Eingabe = int.Parse(Console.ReadLine()!);
                            Console.Clear();

                            switch (Eingabe)
                            {
                                case 0:
                                    Console.Clear();
                                    break;

                                case 1:
                                    Console.WriteLine("Was möchten Sie anzeigen?\n" +
                                                      "1 - DVD\n" +
                                                      "2 - Buch\n");
                                    Console.Write("Ihre Auswahl: ");
                                    Eingabe = int.Parse(Console.ReadLine()!);
                                    Console.Clear();

                                    switch (Eingabe)
                                    {
                                        case 1:
                                            Console.WriteLine("Alle DVDs:\n");

                                            foreach (Dvd D in Service.ReturnDvds())
                                            {
                                                Console.WriteLine($"{D.GetId()} {D.GetTitle()} ({D.GetPubDate().ToShortDateString()}) von {D.GetDirector()}");
                                            }
                                            Console.WriteLine("\nEnter um zum Menü zurückzukehren");
                                            Console.ReadLine();
                                            Console.Clear();

                                            break;

                                        case 2:
                                            Console.WriteLine("Alle Bücher:\n");

                                            foreach (Book B in Service.ReturnBooks())
                                            {
                                                Console.WriteLine($"{B.GetId()} {B.GetTitle()} ({B.GetPubDate().ToShortDateString()}) von {B.GetAuthor()}");
                                            }
                                            Console.WriteLine("\nEnter um zum Hauptmenü zurückzukehren");
                                            Console.ReadLine();
                                            Console.Clear();

                                            break;

                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Keine gültige Auswahl für das Medium getroffen.\nZurück im Hauptmenü.\n");
                                            break;
                                    }
                                    break;

                                case 2:
                                    Console.WriteLine("Alle Kunden:\n");

                                    foreach (Customer K in Service.ReturnCustomers())
                                    {
                                        Console.WriteLine($"ID: {K.GetId()} - {K.GetDetails(K.GetId())[0]}, {K.GetDetails(K.GetId())[1]}");
                                    }
                                    Console.WriteLine("\nEnter um zum Hauptmenü zurückzukehren");
                                    Console.ReadLine();
                                    Console.Clear();

                                    break;

                                case 3:
                                    Console.WriteLine("Alle aktiven Verleihvorgänge:\n");

                                    foreach (Loan L in Service.ReturnLoans())
                                    {
                                        if (L.GetStatus()) // true = aktiver Verleih
                                        {
                                            L.GetInfo();
                                        }
                                    }

                                    Console.WriteLine("\n\nAlle abgeschlossenen Verleihvorgänge:\n");

                                    foreach (Loan L in Service.ReturnLoans())
                                    {
                                        if (!L.GetStatus()) // false == abgeschlossener Verleih
                                        {
                                            L.GetInfo();
                                        }
                                    }

                                    Console.WriteLine("\nEnter um zum Hauptmenü zurückzukehren");
                                    Console.ReadLine();
                                    Console.Clear();

                                    break;
                            }
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Keine gültige Auswahl getroffen!\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Es wurde keine (gültige) Zahl in die Eingabe eingetippt.\nZurück im Hauptmenü.\n");
                }
                catch (OverflowException)
                {
                    Console.Clear();
                    Console.WriteLine("Ein so großer Wert ist hier nicht gefragt...\n");
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}