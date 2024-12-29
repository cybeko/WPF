using System.Collections.Generic;
using System.Windows;
using SampleMVVM.Models;
using SampleMVVM.ViewModels;
using SampleMVVM.Views;

namespace SampleMVVM
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            List<Book> books = new List<Book>()
            {
                new Book("WPF", "Мэтью Мак-Дональд", 5),
                new Book("CLR via C#", "Джеффри Рихтер", 3),
                new Book("C#", "Герберт Шилдт", 7)
            };

            MainView view = new MainView();
            MainViewModel viewModel = new MainViewModel(books);
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
