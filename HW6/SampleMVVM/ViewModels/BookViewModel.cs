using SampleMVVM.Commands;
using SampleMVVM.Models;
using System.Windows;
using System.Windows.Input;

namespace SampleMVVM.ViewModels
{
    class BookViewModel : ViewModelBase
    {
        private Book Book;

        public BookViewModel(Book book)
        {
            this.Book = book;
        }

        public string Title
        {
            get { return Book.Title; }
            set
            {
                Book.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Author
        {
            get { return Book.Author; }
            set
            {
                Book.Author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public int Count
        {
            get { return Book.Count; }
            set
            {
                Book.Count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        private DelegateCommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand(param => AddBook(), null);
                }
                return addCommand;
            }
        }
        public string NewTitle
        {
            get { return _newTitle; }
            set
            {
                _newTitle = value;
                OnPropertyChanged(nameof(NewTitle));
            }
        }

        private string _newTitle = string.Empty;

        public string NewAuthor
        {
            get { return _newAuthor; }
            set
            {
                _newAuthor = value;
                OnPropertyChanged(nameof(NewAuthor));
            }
        }

        private string _newAuthor = string.Empty;


        private void AddBook()
        {
            var newBook = new Book(NewTitle, NewAuthor, 1);

            var mainViewModel = (MainViewModel)Application.Current.MainWindow.DataContext;
            mainViewModel.AddBook(newBook);
            NewTitle = string.Empty;
            NewAuthor = string.Empty;
        }

        private DelegateCommand getItemCommand;
        public ICommand GetItemCommand
        {
            get
            {
                if (getItemCommand == null)
                {
                    getItemCommand = new DelegateCommand(param => this.GetItem(), null);
                }
                return getItemCommand;
            }
        }

        private void GetItem()
        {
            Count++;
        }

        private DelegateCommand giveItemCommand;
        public ICommand GiveItemCommand
        {
            get
            {
                if (giveItemCommand == null)
                {
                    giveItemCommand = new DelegateCommand(param => GiveItem(), param => CanGiveItem());
                }
                return giveItemCommand;
            }
        }
        private void GiveItem()
        {
            Count--;
        }

        private bool CanGiveItem()
        {
            return Count > 0;
        }
    }
}
