using System;

namespace LibrarySystem
{
    public abstract class LibraryItem : IBorrowable
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }

        private static int _totalCount = 0;
        public static int TotalCount => _totalCount;

        private bool _isAvailable = true;

        protected LibraryItem(int id, DateTime dateReceived)
        {
            Id = id;
            DateReceived = dateReceived;
            _totalCount++;
        }

        public bool IsAvailable => _isAvailable;

        public void Borrow()
        {
            if (_isAvailable)
            {
                _isAvailable = false;
                Console.WriteLine($"Предмет ID {Id} выдан");
            }
            else
            {
                Console.WriteLine($"Ошибка: предмет ID {Id} уже выдан");
            }
        }

        public void Return()
        {
            if (!_isAvailable)
            {
                _isAvailable = true;
                Console.WriteLine($"Предмет ID {Id} возвращён");
            }
            else
            {
                Console.WriteLine($"Предмет ID {Id} уже в библиотеке");
            }
        }

        public abstract void PrintInfo();
    }
}