using System;

namespace ConsoleApp1
{
    public class Podcast : MediaItem
    {
        private string _host;
        private int _episodeNumber;

        public string host
        {
            get => _host;
            set => _host = value;
        }

        public int episodeNumber
        {
            get => _episodeNumber;
            set => _episodeNumber = value;
        }

        public Podcast(string title, string author, int year, string host, int episodeNumber)
            : base(title, author, year)
        {
            _host = host;
            _episodeNumber = episodeNumber;
        }

        public override void print()
        {
            base.print();
            Console.WriteLine($"host: {host}");
            Console.WriteLine($"episodeNumber: {episodeNumber}");
        }

        public override void Play()
        {
            Console.WriteLine($" Воспроизведение выпуска #{_episodeNumber} — '{title}' (ведущий: {host})");
        }
    }
}