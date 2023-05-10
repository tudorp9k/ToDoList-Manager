using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ToDoListItem> ItemsCollection { get; set; }
        public FileMenuCommands FileMenuCommands { get; set; }
        private ToDoListRepository toDoListRepository;

        public MainViewModel()
        {
            toDoListRepository = new ToDoListRepository();
            ItemsCollection = toDoListRepository.GetAll();
            FileMenuCommands = new FileMenuCommands(toDoListRepository);
        }
        private ToDoListItem selectedItem;
        public ToDoListItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                FileMenuCommands.SelectedItem = value;
            }
        }

        private TaskItem selectedTask;
        public TaskItem SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                FileMenuCommands.SelectedTask = value;
                NotifyPropertyChanged("SelectedTask");
            }
        }
    }
}
