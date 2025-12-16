using System;
using System.Collections.Generic;
using System.Text;

namespace Klassen
{
    public class Dvd : MediaBase
    {
        private string _director;
        private uint _runtime = 107; // in Minuten

        public Dvd(ref LibraryService service, string title, string director, DateTime pubdate, uint runtime = 0, string desc = "", int units = 0)
        {
            SetId(service.ReturnDvds().Count + 1);
            SetMediaType(MediaType.DVD);

            SetTitle(title);
            _director = director;
            SetPubDate(pubdate);
            if (runtime != 0)
            {
                _runtime = runtime;
            }
            if (desc != "")
            {
                SetDescription(desc);
            }
            if (units != 0)
            {
                SetUnits(units);
            }
        }
        public string GetDirector()
        {
            return _director; 
        }

        public string GetRuntime()
        {
            uint stunde = _runtime / 60;
            double minute = Math.Ceiling((_runtime / 60.0 - stunde) * 60);

            switch(stunde)
            {
                case > 1:
                    return $"{stunde} Stunden {minute} Minuten";
                case 1:
                    return $"{stunde} Stunde {minute} Minuten";
                case 0:
                    return $"{minute} Minuten";
            }
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Film-ID: {GetId()}\n" +
                              $"Titel: {GetTitle()}\n" +
                              $"Director: {GetDirector()}\n" +
                              $"Veröffentlichung: {GetPubDate()}\n" +
                              $"Laufzeit: {GetRuntime()}\n" +
                              $"Momentan verleihbare Einheiten: {GetUnits()}\n" +
                              $"Beschreibung: {GetDescription()}\n");
        }
    }
}
