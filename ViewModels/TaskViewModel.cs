using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Views;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        private UpdateTaskView view;
        private TaskItem selectedTask;
        private ToDoListItem selectedItem;
        private ToDoListRepository toDoListRepository;
        private CommandItemType type;
        public ICommand OkCommand { get; set; }

        public TaskViewModel(UpdateTaskView view, TaskItem selectedTask, ToDoListItem selectedItem, ToDoListRepository toDoListRepository, CommandItemType type)
        {
            this.view = view;
            this.selectedTask = selectedTask;
            this.selectedItem = selectedItem;
            this.toDoListRepository = toDoListRepository;
            this.type = type;

            TaskPriorities = new ObservableCollection<TaskPriority>();
            TaskPriorities.Add(TaskPriority.High);
            TaskPriorities.Add(TaskPriority.Medium);
            TaskPriorities.Add(TaskPriority.Low);

            TaskCategories = new ObservableCollection<TaskCategory>();
            TaskCategories.Add(TaskCategory.Study);
            TaskCategories.Add(TaskCategory.Work);
            TaskCategories.Add(TaskCategory.Home);
            TaskCategories.Add(TaskCategory.Personal);

            DueDate = DateTime.Today;

            OkCommand = new RelayCommand(Ok);
        }

        private void Ok()
        {
            // force update of binding
            var expression = view.txtBoxName.GetBindingExpression(TextBox.TextProperty);
            expression.UpdateSource();

            expression = view.txtBoxDescription.GetBindingExpression(TextBox.TextProperty);
            expression.UpdateSource();

            expression = view.comboBoxPriority.GetBindingExpression(ComboBox.SelectedItemProperty);
            expression.UpdateSource();

            expression = view.comboBoxCategory.GetBindingExpression(ComboBox.SelectedItemProperty);
            expression.UpdateSource();

            expression = view.datePickerDueDate.GetBindingExpression(DatePicker.SelectedDateProperty);
            expression.UpdateSource();

            var newTask = new TaskItem(Name, Description, TaskPriority, TaskCategory, DueDate);
            switch (type)
            {
                case CommandItemType.AddTask:
                    toDoListRepository.AddTask(newTask, selectedItem);
                    break;
                case CommandItemType.Edit:
                    if (selectedTask != null)
                        toDoListRepository.EditTask(newTask, selectedTask, selectedItem);
                    break;
            }

            CloseAction();
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

        private ObservableCollection<TaskPriority> taskPriorities;
        public ObservableCollection<TaskPriority> TaskPriorities
        {
            get { return taskPriorities; }
            set
            {
                taskPriorities = value;
                NotifyPropertyChanged("TaskPriorities");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private TaskCategory taskCategory;
        public TaskCategory TaskCategory
        {
            get { return taskCategory; }
            set
            {
                taskCategory = value;
                NotifyPropertyChanged("TaskCategory");
            }
        }

        private TaskPriority taskPriority;
        public TaskPriority TaskPriority
        {
            get { return taskPriority; }
            set
            {
                taskPriority = value;
                NotifyPropertyChanged("TaskPriority");
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
            }
        }

    }
}
