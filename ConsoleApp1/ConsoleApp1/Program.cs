using LibrarySystem;


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
