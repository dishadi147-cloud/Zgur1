using System;

namespace LibrarySystem
{
    public abstract class LibraryItem : IBorrowable
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime? DueDate { get; protected set; }

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
                DueDate = DateTime.Now.AddDays(30); 
                Console.WriteLine($"Предмет ID {Id} выдан. Срок возврата: {DueDate.Value.ToShortDateString()}");
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
                DueDate = null;
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