namespace LibrarySystem
{
    public interface IBorrowable
    {
        void Borrow();
        void Return();
        bool IsAvailable { get; }
    }
}
