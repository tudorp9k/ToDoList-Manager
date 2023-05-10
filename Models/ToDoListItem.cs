using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoList.ViewModels;

namespace ToDoList.Models
{
    public class ToDoListItem : ViewModelBase
    {   
        public ToDoListItem(string itemName, string imagePath)
        {
            ItemName = itemName;
            ImagePath = imagePath;
        }

        private ObservableCollection<TaskItem> taskSubCollection;
        public ObservableCollection<TaskItem> TaskSubCollection
        {
            get
            {
                return taskSubCollection;
            }
            set
            {
                taskSubCollection = value;
                NotifyPropertyChanged("TaskSubCollection");
            }
        }

        private ObservableCollection<ToDoListItem> toDoListSubCollection;
        public ObservableCollection<ToDoListItem> ToDoListSubCollection
        {
            get
            {
                return toDoListSubCollection;
            }
            set
            {
                toDoListSubCollection = value;
                NotifyPropertyChanged("ToDoListSubCollection");
            }
        }
        
        private string itemName;
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                NotifyPropertyChanged("ItemName");
            }
        }
        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                NotifyPropertyChanged("ImagePath");
            }
        }
    }
}