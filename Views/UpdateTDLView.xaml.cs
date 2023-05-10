using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Services;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for UpdateTDLView.xaml
    /// </summary>
    public partial class UpdateTDLView : Window
    {
        public UpdateTDLView(ToDoListItem selectedItem, ToDoListRepository toDoListRepository, CommandItemType type)
        {
            InitializeComponent();
            var vm = new TDLViewModel(this, selectedItem, toDoListRepository, type);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

    }
}
