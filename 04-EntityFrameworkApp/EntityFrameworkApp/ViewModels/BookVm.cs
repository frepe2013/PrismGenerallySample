namespace EntityFrameworkApp.ViewModels
{
    public class BookVm
    {
        public string Title { get; }

        public string Author { get; }

        public BookVm(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
