using System;

namespace ConsoleApp1
{
    public class Song : MediaItem
    {
        private string _artist;
        private string _album;
        private int _durationSeconds;
        private string _genre;
        private int _bitrate;

        public string artist
        {
            get => _artist;
            set => _artist = value;
        }

        public string album
        {
            get => _album;
            set => _album = value;
        }

        public int durationSeconds
        {
            get => _durationSeconds;
            set => _durationSeconds = value;
        }

        public string genre
        {
            get => _genre;
            set => _genre = value;
        }

        public int bitrate
        {
            get => _bitrate;
            set => _bitrate = value;
        }

        public Song(string title, string author, int year, string artist, string album,
                    int durationSeconds, string genre, int bitrate)
            : base(title, author, year)
        {
            _artist = artist;
            _album = album;
            _durationSeconds = durationSeconds;
            _genre = genre;
            _bitrate = bitrate;
        }

        public override void print()
        {
            base.print();
            Console.WriteLine($"artist: {artist}");
            Console.WriteLine($"album: {album}");
            Console.WriteLine($"duration (sec): {durationSeconds}");
            Console.WriteLine($"genre: {genre}");
            Console.WriteLine($"bitrate: {bitrate} kbps");
        }

        public override void Play()
        {
            Console.WriteLine($"Воспроизведение: {title} - {artist}");
        }

        public void Pause()
        {
            Console.WriteLine($"Пауза: {title} - {artist}");
        }

        public void GetQualityInfo()
        {
            string quality;
            if (_bitrate >= 320) quality = "Отличное";
            else if (_bitrate >= 256) quality = "Хорошее";
            else if (_bitrate >= 192) quality = "Среднее";
            else if (_bitrate >= 128) quality = "Приемлемое";
            else quality = "Низкое";

            Console.WriteLine($"Качество трека '{title}': {quality} (битрейт: {_bitrate} kbps)");
        }
    }
}