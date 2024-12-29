using Newtonsoft.Json;
using SampleMVVM.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace SampleMVVM.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<BookViewModel> BooksList { get; set; }

        private const string JsonFilePath = "books.json";

        public MainViewModel(List<Book> books)
        {
            LoadBooksFromJson();

            if (BooksList == null || !BooksList.Any())
            {
                BooksList = new ObservableCollection<BookViewModel>(books.Select(b => new BookViewModel(b)));
            }
        }

        public void AddBook(Book newBook)
        {
            BooksList.Add(new BookViewModel(newBook));
            SaveBooksToJson(); 
        }

        private void SaveBooksToJson()
        {
            var books = BooksList.Select(b => new Book(b.Title, b.Author, b.Count)).ToList();
            var json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(JsonFilePath, json);
        }

        private void LoadBooksFromJson()
        {
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath);
                var books = JsonConvert.DeserializeObject<List<Book>>(json);
                if (books != null)
                {
                    BooksList = new ObservableCollection<BookViewModel>(books.Select(b => new BookViewModel(b)));
                }
            }
        }
    }
}
