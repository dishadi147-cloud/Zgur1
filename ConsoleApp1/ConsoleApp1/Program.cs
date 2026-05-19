using LibrarySystem;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {


            PhysicalBook book1 = new PhysicalBook(1, DateTime.Parse("2024-01-15"), "Просвещение", 350);
            PhysicalBook book2 = new PhysicalBook(2, DateTime.Parse("2024-02-20"), "Эксмо", 280);
            EBook ebook1 = new EBook(3, DateTime.Parse("2024-03-10"), "PDF", 5.2);
            EBook ebook2 = new EBook(4, DateTime.Parse("2024-03-15"), "EPUB", 3.8);

            LibraryItem[] items = new LibraryItem[] { book1, book2, ebook1 };
            Library<LibraryItem> library = new Library<LibraryItem>(items);

            library.PrintAllItems();

            LibraryItem? found = library.FindById(2);
            if (found != null)
            {
                Console.WriteLine($"\nНайден предмет с ID=2:");
                found.PrintInfo();
            }
            else
            {
                Console.WriteLine("Не найден");
            }

            Console.WriteLine("\nВыдача книги ID=1:");
            library.BorrowItem(book1);

            Console.WriteLine("\nДоступные предметы:");
            List<LibraryItem> available = library.GetAvailableItems();
            foreach (var item in available)
            {
                item.PrintInfo();
            }

            Console.WriteLine("\nВозврат книги ID=1:");
            book1.Return();

            Console.WriteLine("\nПосле добавления ещё одного предмета:");
            EBook ebook3 = new EBook(5, DateTime.Now, "MOBI", 4.5);
            library.AddItem(ebook3);
            library.PrintAllItems();


            Console.WriteLine("\n\n перегрузка операторов и методы расширения\n");


            Library<LibraryItem> library2 = new Library<LibraryItem>(new LibraryItem[] { book1, book2, ebook1 });

            Console.WriteLine("1. Метод расширения CountAvailable");
            Console.WriteLine($"Доступно предметов в библиотеке: {library2.CountAvailable()}");

            Console.WriteLine("\n2. Выдача предмета ID=1");
            library2.BorrowItem(book1);
            Console.WriteLine($"Доступно предметов после выдачи: {library2.CountAvailable()}");

            Console.WriteLine("\n3. Метод расширения IsOverdue");
            Console.WriteLine($"Предмет ID=1 (выдан) просрочен на 35 дней? {book1.IsOverdue(DateTime.Now.AddDays(35))}");
            Console.WriteLine($"Предмет ID=2 (не выдан) просрочен? {book2.IsOverdue(DateTime.Now.AddDays(35))}");

            Console.WriteLine("\n4. Оператор + (добавление предмета)");
            Console.WriteLine("Добавляем ebook2 с ID=4:");
            library2 += ebook2;
            library2.PrintAllItems();

            Console.WriteLine("\n5. Оператор - (удаление предмета)");
            Console.WriteLine("Удаляем book2 с ID=2:");
            library2 -= book2;
            library2.PrintAllItems();

            Console.WriteLine("\n6. Проверка доступности после всех операций");
            Console.WriteLine($"Доступно предметов: {library2.CountAvailable()}");

            Console.WriteLine("\n7. Возврат предмета ID=1");
            book1.Return();
            Console.WriteLine($"Доступно предметов после возврата: {library2.CountAvailable()}");

            Console.WriteLine("\n8. Проверка IsOverdue для возвращённого предмета");
            Console.WriteLine($"Предмет ID=1 (возвращён) просрочен? {book1.IsOverdue(DateTime.Now.AddDays(35))}");


            Console.ReadKey();
        }
    }
}