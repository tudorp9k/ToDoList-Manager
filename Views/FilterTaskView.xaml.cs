using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for FilterTaskView.xaml
    /// </summary>
    public partial class FilterTaskView : Window
    {
        public FilterTaskView(ObservableCollection<ToDoListItem> items)
        {
            InitializeComponent();
            DataContext = new FilterTaskViewModel(items);
        }
    }
}
