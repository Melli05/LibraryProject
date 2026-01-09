using System.Xml.Serialization;

namespace Klassen
{
    public class MediaBase
    {
        private int _id;
        private string _title;
        private string _description = "Keine Beschreibung verfügbar.";
        private DateTime _pubdate;
        private MediaType _type;
        private int _units = 1;

        public void SetId(int id)
        {
            try
            {
                if (id > 0)
                {
                    this._id = id;
                }
                else if(id == -1) // für Placeholders
                {
                    this._id = id;
                }
                else
                {
                    throw new Exception("ID des Mediums kann nicht kleiner oder gleich 0 sein.");
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
        public void SetUnits(int units)
        {
            _units = units;
        }
        public int GetUnits()
        {
            return _units;
        }
        public void SetTitle(string title)
        {
            _title = title;
        }
        public string GetTitle()
        {
            return _title;
        }
        public void SetPubDate(DateTime pubdate)
        {
            _pubdate = pubdate;
        }
        public DateTime GetPubDate()
        {
            return _pubdate;
        }
        public void SetMediaType(MediaType media)
        {
            _type = media;
        }
        public MediaType GetMediaType()
        {
            return _type;
        }
        public void SetDescription(string desc)
        {
            _description = desc; 
        }
        public string GetDescription()
        {
            return _description;
        }
        public virtual void PrintInfo()
        {
            Console.WriteLine("Zu diesem Artikel stehen keine Informationen zur Verfügung.");
        }
    }
}
