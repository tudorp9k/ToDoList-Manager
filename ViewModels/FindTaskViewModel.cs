using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class FindTaskViewModel : ViewModelBase
    {
        public ObservableCollection<TaskItem> ItemsCollection { get; set; }
        private TaskFilters taskFilters;
        public FindTaskViewModel(ObservableCollection<ToDoListItem> itemsCollection)
        {
            taskFilters = new TaskFilters(itemsCollection);
            DueDate = DateTime.Now;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
                ItemsCollection = taskFilters.FindTasksByName(name);
                NotifyPropertyChanged("ItemsCollection");
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                NotifyPropertyChanged("DueDate");
                ItemsCollection = taskFilters.FindTasksByDueDate(dueDate);
                NotifyPropertyChanged("ItemsCollection");
            }
        }
    }
}
