using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    internal class TaskStatistics
    {
        private readonly ObservableCollection<ToDoListItem> ItemsCollection;
        public TaskStatistics(ObservableCollection<ToDoListItem> ItemsCollection)
        {
            this.ItemsCollection = ItemsCollection ?? throw new ArgumentNullException(nameof(ItemsCollection));
        }

        public int GetTasksDueTodayCount()
        {
            return GetTasksDueOnDateCount(DateTime.Now);
        }

        public int GetTasksDueTomorrowCount()
        {
            return GetTasksDueOnDateCount(DateTime.Now.AddDays(1));
        }

        public int GetTasksOverdueCount()
        {
            int count = 0;
            var stack = new Stack<ToDoListItem>(ItemsCollection);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var task in item.TaskSubCollection)
                    {
                        if (task.DueDate.Date < task.FinishDate && !task.IsDone)
                        {
                            count++;
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
            return count;
        }

        public int GetTasksDueOnDateCount(DateTime date)
        {
            int count = 0;
            var stack = new Stack<ToDoListItem>(ItemsCollection);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var task in item.TaskSubCollection)
                    {
                        if (task.DueDate.Date == date.Date && !task.IsDone)
                        {
                            count++;
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
            return count;
        }

        public int GetTasksDoneCount()
        {
            int count = 0;
            var stack = new Stack<ToDoListItem>(ItemsCollection);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var task in item.TaskSubCollection)
                    {
                        if (task.IsDone)
                        {
                            count++;
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
            return count;
        }

        public int GetTasksNotDoneCount()
        {
            int count = 0;
            var stack = new Stack<ToDoListItem>(ItemsCollection);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.TaskSubCollection != null)
                {
                    foreach (var task in item.TaskSubCollection)
                    {
                        if (!task.IsDone)
                        {
                            count++;
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
            return count;
        }
    }
}
