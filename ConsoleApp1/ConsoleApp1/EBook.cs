using System;

namespace LibrarySystem
{
    public class EBook : LibraryItem
    {
        public string Format { get; set; }
        public double SizeMB { get; set; }

        public EBook(int id, DateTime dateReceived, string format, double sizeMB)
            : base(id, dateReceived)
        {
            Format = format;
            SizeMB = sizeMB;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Электронная книга: ID={Id}, Формат={Format}, Размер={SizeMB}MB, Доступна={IsAvailable}");
        }
    }
}