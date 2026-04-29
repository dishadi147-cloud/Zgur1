using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem
{
    public class Library<T> where T : LibraryItem
    {
        private List<T> _items = new List<T>();

        public Library(IEnumerable<T>? items = null)
        {
            if (items != null)
                _items.AddRange(items);
        }

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public void BorrowItem(T item)
        {
            item.Borrow();
        }

        public List<T> GetAvailableItems()
        {
            return _items.Where(item => item.IsAvailable).ToList();
        }

        public void PrintAllItems()
        {
            Console.WriteLine($"Всего создано предметов: {LibraryItem.TotalCount}");
            Console.WriteLine("Список предметов в библиотеке:");
            foreach (var item in _items)
            {
                item.PrintInfo();
            }
        }

        public T? FindById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }
    }
}
