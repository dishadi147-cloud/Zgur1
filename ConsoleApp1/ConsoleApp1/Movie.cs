using System;

namespace ConsoleApp1
{
    public class Movie : MediaItem
    {
        private string _director;
        private double _rating;

        public string director
        {
            get => _director;
            set => _director = value;
        }

        public double rating
        {
            get => _rating;
            set => _rating = value;
        }

        public Movie(string title, string author, int year, string director, double rating)
            : base(title, author, year)
        {
            _director = director;
            _rating = rating;
        }

        public override void print()
        {
            base.print();
            Console.WriteLine($"director: {director}");
            Console.WriteLine($"rating: {rating}");
        }

        public override void Play()
        {
            Console.WriteLine($" Запуск фильма '{title}' (реж. {director}, рейтинг: {rating})");
        }
    }
}
