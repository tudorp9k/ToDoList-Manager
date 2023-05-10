using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskFilters
    {
        private readonly ObservableCollection<ToDoListItem> items;
        public TaskFilters(ObservableCollection<ToDoListItem> items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));
        }
        private ObservableCollection<TaskItem> GetAllTasks()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        tasks.Add(taskitem);
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> GetTasksSortedByPriority()
        {
            var tasks = GetAllTasks();
            var sortedTasks = tasks.OrderBy(t => t.Priority);
            return new ObservableCollection<TaskItem>(sortedTasks);
        }
        public ObservableCollection<TaskItem> GetTasksSortedByDueDate()
        {
            var tasks = GetAllTasks();
            var sortedTasks = tasks.OrderBy(t => t.DueDate);
            return new ObservableCollection<TaskItem>(sortedTasks);
        }

        public ObservableCollection<TaskItem> FindTasksByName(string name)
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (taskitem.Title == name)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindTasksByDueDate(DateTime dueDate)
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (taskitem.DueDate.Date == dueDate.Date)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindTasksByCategory(TaskCategory category)
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (taskitem.Category == category)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindTasksDone()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (taskitem.IsDone)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindAvailableTasksNotDone()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (!taskitem.IsDone && taskitem.DueDate.Date > DateTime.Now.Date)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindNotAvailableTasksNotDone()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (!taskitem.IsDone && taskitem.DueDate.Date < DateTime.Now.Date)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
        public ObservableCollection<TaskItem> FindTasksOverdue()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var taskitem in item.TaskSubCollection)
                    {
                        if (taskitem.DueDate.Date < DateTime.Now.Date)
                        {
                            tasks.Add(taskitem);
                        }
                    }
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
            return tasks;
        }
    }
}
