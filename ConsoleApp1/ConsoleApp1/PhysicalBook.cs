using System;

namespace LibrarySystem
{
    public class PhysicalBook : LibraryItem
    {
        public string Publisher { get; set; }
        public int Pages { get; set; }

        public PhysicalBook(int id, DateTime dateReceived, string publisher, int pages)
            : base(id, dateReceived)
        {
            Publisher = publisher;
            Pages = pages;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Физическая книга: ID={Id}, Издательство={Publisher}, Страниц={Pages}, Доступна={IsAvailable}");
        }
    }
}