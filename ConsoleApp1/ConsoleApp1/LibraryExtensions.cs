using System;
using System.Linq;

namespace LibrarySystem
{
    public static class LibraryExtensions
    {

        public static bool IsOverdue(this LibraryItem item, DateTime currentDate)
        {
            if (item.IsAvailable)
                return false;

            if (!item.DueDate.HasValue)
                return false;

            return currentDate > item.DueDate.Value;
        }


        public static int CountAvailable<T>(this Library<T> library) where T : LibraryItem
        {
            return library.GetAvailableItems().Count;
        }
    }
}