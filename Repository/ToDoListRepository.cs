using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ToDoList.Models;

namespace ToDoList.Repository
{
    public class ToDoListRepository
    {
        private ObservableCollection<ToDoListItem> items;
        public ToDoListRepository()
        {
            items = new ObservableCollection<ToDoListItem>();

            var homeList = new ToDoListItem("Home Tasks", "/Resources/home.png")
            {
                TaskSubCollection = new ObservableCollection<TaskItem>()
                {
                    new TaskItem("Cleaning", "Vacuum the living room", TaskPriority.High, TaskCategory.Home, DateTime.Now.AddDays(1)),
                    new TaskItem("Shopping", "Buy groceries for the week", TaskPriority.Medium, TaskCategory.Home, DateTime.Now.AddDays(2)),
                    new TaskItem("Pets", "Take the dog for a walk", TaskPriority.Medium, TaskCategory.Home, DateTime.Now.AddDays(3)),
                }
            };
            items.Add(homeList);
            
            var workList = new ToDoListItem("Work Tasks", "/Resources/work.png")
            {
                TaskSubCollection = new ObservableCollection<TaskItem>()
                {
                    new TaskItem("Meeting", "Prepare for weekly team meeting", TaskPriority.High, TaskCategory.Work, DateTime.Now.AddDays(1)),
                    new TaskItem("Project", "Finish coding the feature X", TaskPriority.High, TaskCategory.Work, DateTime.Now.AddDays(2)),
                    new TaskItem("Emails", "Respond to emails from clients", TaskPriority.Medium, TaskCategory.Work, DateTime.Now.AddDays(3)),
                },
                ToDoListSubCollection = new ObservableCollection<ToDoListItem>()
                {
                    new ToDoListItem("Design Tasks", "/Resources/design.png")
                    {
                        TaskSubCollection = new ObservableCollection<TaskItem>()
                        {
                            new TaskItem("Video", "Create a new video for client", TaskPriority.High, TaskCategory.Work, DateTime.Now.AddDays(4)),
                            new TaskItem("Edit", "Design mockups for new feature Z", TaskPriority.Medium, TaskCategory.Work, DateTime.Now.AddDays(5)),
                        }
                    },
                    new ToDoListItem("Development Tasks", "/Resources/code.png")
                    {
                        TaskSubCollection = new ObservableCollection<TaskItem>()
                        {
                            new TaskItem("Bug", "Fix the bug on page X", TaskPriority.High, TaskCategory.Work, DateTime.Now.AddDays(6)),
                            new TaskItem("Optimization", "Optimize the database queries", TaskPriority.Medium, TaskCategory.Work, DateTime.Now.AddDays(7)),
                        }
                    }
                }
            };
            items.Add(workList);

            var personalList = new ToDoListItem("Personal Tasks", "/Resources/personal.png")
            {
                TaskSubCollection = new ObservableCollection<TaskItem>()
                {
                    new TaskItem("Health", "Go for a run in the park", TaskPriority.Low, TaskCategory.Personal, DateTime.Now.AddDays(1)),
                    new TaskItem("Hobby", "Practice playing guitar for 30 minutes", TaskPriority.Medium, TaskCategory.Personal, DateTime.Now.AddDays(2)),
                    new TaskItem("Friends", "Call John and ask about his new job", TaskPriority.Low, TaskCategory.Personal, DateTime.Now.AddDays(3)),
                }
            };
            items.Add(personalList);

            var studyList = new ToDoListItem("Study Tasks", "/Resources/books.png")
            {
                TaskSubCollection = new ObservableCollection<TaskItem>()
                {
                    new TaskItem("Assignment", "Finish the math assignment", TaskPriority.High, TaskCategory.Study, DateTime.Now.AddDays(1)),
                    new TaskItem("Reading", "Read chapters 5-6 from the book", TaskPriority.Medium, TaskCategory.Study, DateTime.Now.AddDays(2)),
                    new TaskItem("Presentation", "Prepare slides for the group presentation", TaskPriority.Medium, TaskCategory.Study, DateTime.Now.AddDays(3)),
                },
                ToDoListSubCollection = new ObservableCollection<ToDoListItem>()
                {
                    new ToDoListItem("Computer Science Tasks", "/Resources/computer.png")
                    {
                        TaskSubCollection = new ObservableCollection<TaskItem>()
                        {
                            new TaskItem("Algorithm", "Implement the quicksort algorithm", TaskPriority.High, TaskCategory.Study, DateTime.Now.AddDays(4)),
                            new TaskItem("Data Structures", "Study linked lists and stacks", TaskPriority.Medium, TaskCategory.Study, DateTime.Now.AddDays(5)),
                        }
                    },
                    new ToDoListItem("Foreign Language Tasks", "/Resources/language.png")
                    {
                        TaskSubCollection = new ObservableCollection<TaskItem>()
                        {
                            new TaskItem("Vocabulary", "Learn 20 new words in Spanish", TaskPriority.Low, TaskCategory.Study, DateTime.Now.AddDays(6)),
                            new TaskItem("Grammar", "Practice verb conjugation", TaskPriority.Medium, TaskCategory.Study, DateTime.Now.AddDays(7)),
                        }
                    }
                }
            };
            items.Add(studyList);
        }

        public ObservableCollection<ToDoListItem> GetAll() => items;

        public void AddSubTDL(ToDoListItem newItem, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.ToDoListSubCollection == null)
                    {
                        item.ToDoListSubCollection = new ObservableCollection<ToDoListItem>();
                    }
                    item.ToDoListSubCollection.Add(newItem);
                    return;
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
        }

        public void DeleteTDL(ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }

                if (item.ToDoListSubCollection != null && item.ToDoListSubCollection.Contains(selectedItem))
                {
                    item.ToDoListSubCollection.Remove(selectedItem);
                    return;
                }
                else if (item == selectedItem)
                {
                    items.Remove(item);
                    return;
                }
            }
        }

        public void MoveUpSubTDL(ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == selectedItem)
                {
                    return;
                }
                if (current.ToDoListSubCollection != null)
                {
                    foreach (var subitem in current.ToDoListSubCollection)
                    {
                        if (subitem == selectedItem)
                        {
                            var index = current.ToDoListSubCollection.IndexOf(subitem);
                            if (index - 1 < 0)
                            {
                                return;
                            }
                            current.ToDoListSubCollection.RemoveAt(index);
                            current.ToDoListSubCollection.Insert(index - 1, subitem);
                            return;
                        }
                        stack.Push(subitem);
                    }
                }
            }
        }

        public void MoveDownSubTDL(ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == selectedItem)
                {
                    return;
                }
                if (current.ToDoListSubCollection != null)
                {
                    foreach (var subitem in current.ToDoListSubCollection)
                    {
                        if (subitem == selectedItem)
                        {
                            var index = current.ToDoListSubCollection.IndexOf(subitem);
                            if (index + 1 >= current.ToDoListSubCollection.Count)
                            {
                                return;
                            }
                            current.ToDoListSubCollection.RemoveAt(index);
                            current.ToDoListSubCollection.Insert(index + 1, subitem);
                            return;
                        }
                        stack.Push(subitem);
                    }
                }
            }
        }

        public void EditTDL(ToDoListItem newItem, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    item.ItemName = newItem.ItemName;
                    item.ImagePath = newItem.ImagePath;
                    return;
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
        }

        public void MoveUpTDL(ToDoListItem item)
        {
            var index = items.IndexOf(item);
            if (index - 1 < 0)
                return;
            items.RemoveAt(index);
            items.Insert(index - 1, item);
        }

        public void MoveDownTDL(ToDoListItem item)
        {
            var index = items.IndexOf(item);
            if (index + 1 >= items.Count)
                return;
            items.RemoveAt(index);
            items.Insert(index + 1, item);
        }

        public void AddRootTDL(ToDoListItem newItem)
        {
            items.Add(newItem);
        }

        public void AddTask(TaskItem newTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection == null)
                    {
                        item.TaskSubCollection = new ObservableCollection<TaskItem>();
                    }
                    item.TaskSubCollection.Add(newTask);
                    return;
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
        }
        public void EditTask(TaskItem newTask, TaskItem selectedTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection.Contains(selectedTask))
                    {
                        int index = item.TaskSubCollection.IndexOf(selectedTask);
                        item.TaskSubCollection[index] = newTask;
                    }
                    return;
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
        }
        public void DeleteTask(TaskItem selectedTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection.Remove(selectedTask))
                    {
                        return;
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
        }
        public void MoveUpTask(TaskItem selectedTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection.Contains(selectedTask))
                    {
                        var index = item.TaskSubCollection.IndexOf(selectedTask);
                        if (index - 1 < 0)
                        {
                            return;
                        }
                        item.TaskSubCollection.RemoveAt(index);
                        item.TaskSubCollection.Insert(index - 1, selectedTask);
                        return;
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
        }
        public void MoveDownTask(TaskItem selectedTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection.Contains(selectedTask))
                    {
                        var index = item.TaskSubCollection.IndexOf(selectedTask);
                        if (index + 1 >= item.TaskSubCollection.Count)
                        {
                            return;
                        }
                        item.TaskSubCollection.RemoveAt(index);
                        item.TaskSubCollection.Insert(index + 1, selectedTask);
                        return;
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
        }
        public void SetTaskDone(TaskItem selectedTask, ToDoListItem selectedItem)
        {
            var stack = new Stack<ToDoListItem>(items);
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == selectedItem)
                {
                    if (item.TaskSubCollection.Contains(selectedTask))
                    {
                        int index = item.TaskSubCollection.IndexOf(selectedTask);
                        item.TaskSubCollection[index].IsDone = true;
                    }
                    return;
                }
                if (item.ToDoListSubCollection != null)
                {
                    foreach (var subitem in item.ToDoListSubCollection)
                    {
                        stack.Push(subitem);
                    }
                }
            }
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
    }
}
