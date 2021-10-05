using System;
namespace Models
{
    public class Beer
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Tagline { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public float Abv { get; set; }

    }
}