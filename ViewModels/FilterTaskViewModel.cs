using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class FilterTaskViewModel : ViewModelBase
    {
        public ObservableCollection<TaskItem> ItemsCollection { get; set; }
        private TaskFilters taskFilters;

        public ICommand DoneCommand { get; set; }
        public ICommand AvailableNotDoneCommand { get; set; }
        public ICommand NotAvailableNotDoneCommand { get; set; }
        public ICommand OverdueCommand { get; set; }

        public FilterTaskViewModel(ObservableCollection<ToDoListItem> itemsCollection)
        {
            TaskCategories = new ObservableCollection<TaskCategory>();
            TaskCategories.Add(TaskCategory.None);
            TaskCategories.Add(TaskCategory.Study);
            TaskCategories.Add(TaskCategory.Work);
            TaskCategories.Add(TaskCategory.Home);
            TaskCategories.Add(TaskCategory.Personal);

            taskFilters = new TaskFilters(itemsCollection);

            DoneCommand = new RelayCommand(TaskDone);
            AvailableNotDoneCommand = new RelayCommand(TaskAvailableNotDone);
            NotAvailableNotDoneCommand = new RelayCommand(TaskNotAvailableNotDone);
            OverdueCommand = new RelayCommand(TaskOverdue);
        }

        private void TaskOverdue()
        {
            ItemsCollection = taskFilters.FindTasksOverdue();
            NotifyPropertyChanged("ItemsCollection");
        }

        private void TaskNotAvailableNotDone()
        {
            ItemsCollection = taskFilters.FindNotAvailableTasksNotDone();
            NotifyPropertyChanged("ItemsCollection");
        }

        private void TaskAvailableNotDone()
        {
            ItemsCollection = taskFilters.FindAvailableTasksNotDone();
            NotifyPropertyChanged("ItemsCollection");
        }

        private void TaskDone()
        {
            ItemsCollection = taskFilters.FindTasksDone();
            NotifyPropertyChanged("ItemsCollection");
        }

        private TaskCategory taskCategory;
        public TaskCategory TaskCategory
        {
            get { return taskCategory; }
            set
            {
                taskCategory = value;
                NotifyPropertyChanged("TaskCategory");
                ItemsCollection = taskFilters.FindTasksByCategory(taskCategory);
                NotifyPropertyChanged("ItemsCollection");
            }
        }

        private ObservableCollection<TaskCategory> taskCategories;
        public ObservableCollection<TaskCategory> TaskCategories
        {
            get { return taskCategories; }
            set
            {
                taskCategories = value;
                NotifyPropertyChanged("TaskCategories");
            }
        }
    }
}
