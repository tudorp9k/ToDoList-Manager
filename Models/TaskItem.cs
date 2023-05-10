using Microsoft.VisualBasic;
using System;
using ToDoList.ViewModels;

namespace ToDoList.Models
{
    public class TaskItem : ViewModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskCategory Category { get; set; }

        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value;
                NotifyPropertyChanged("IsDone");
            }

        }
        public TaskItem(string Title, string Description, TaskPriority Priority, TaskCategory Category, DateTime DueDate)
        {
            this.Title = Title;
            this.Description = Description;
            this.Priority = Priority;
            this.Category = Category;
            this.DueDate = DueDate;
            this.IsDone = false;
        }
    }
}
