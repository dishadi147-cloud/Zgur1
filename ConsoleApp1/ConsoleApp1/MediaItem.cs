using System;

namespace ConsoleApp1
{
    public abstract class MediaItem
    {
        private string _title;
        protected string _author;
        private int _year;

        public string title
        {
            get => _title;
            set => _title = value;
        }

        public string author
        {
            get => _author;
            set => _author = value;
        }

        public int year
        {
            get => _year;
            set => _year = value;
        }

        protected MediaItem(string title, string author, int year)
        {
            _title = title;
            _author = author;
            _year = year;
        }

        public virtual void print()
        {
            Console.WriteLine($"title: {title}");
            Console.WriteLine($"author: {author}");
            Console.WriteLine($"year: {year}");
        }

        public abstract void Play();
    }
}
