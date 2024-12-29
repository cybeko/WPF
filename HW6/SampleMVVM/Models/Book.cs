namespace SampleMVVM.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }

        public Book(string title, string author, int count)
        {
            Title = title;
            Author = author;
            Count = count;
        }
    }
}
