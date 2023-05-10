using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class SortTaskViewModel : ViewModelBase
    {
        public ObservableCollection<TaskItem> ItemsCollection { get; set; }
        private TaskFilters taskFilters;

        public ICommand SortByDueDateCommand { get; set; }
        public ICommand SortByPriorityCommand { get; set; }

        public SortTaskViewModel(ObservableCollection<ToDoListItem> itemsCollection)
        {
            taskFilters = new TaskFilters(itemsCollection);

            SortByDueDateCommand = new RelayCommand(SortByDueDate);
            SortByPriorityCommand = new RelayCommand(SortByPriority);
        }

        private void SortByPriority()
        {
            ItemsCollection = taskFilters.GetTasksSortedByPriority();
            NotifyPropertyChanged("ItemsCollection");
        }

        private void SortByDueDate()
        {
            ItemsCollection = taskFilters.GetTasksSortedByDueDate();
            NotifyPropertyChanged("ItemsCollection");
        }
    }
}
