using System.Collections.ObjectModel;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    internal class StatisticsViewModel : ViewModelBase
    {
        private TaskStatistics taskStatistics;
        public StatisticsViewModel(ObservableCollection<ToDoListItem> itemsCollection)
        {
            taskStatistics = new TaskStatistics(itemsCollection);

            TasksDueTodayCount = taskStatistics.GetTasksDueTodayCount();
            TasksDueTomorrowCount = taskStatistics.GetTasksDueTomorrowCount();
            TasksOverdueCount = taskStatistics.GetTasksOverdueCount();
            TasksDoneCount = taskStatistics.GetTasksDoneCount();
            TasksNotDoneCount = taskStatistics.GetTasksNotDoneCount();
        }

        private int tasksDueTodayCount;
        public int TasksDueTodayCount
        {
            get { return tasksDueTodayCount; }
            set
            {
                tasksDueTodayCount = value;
                NotifyPropertyChanged("TasksDueTodayCount");
            }
        }

        private int tasksDueTomorrowCount;
        public int TasksDueTomorrowCount
        {
            get { return tasksDueTomorrowCount; }
            set
            {
                tasksDueTomorrowCount = value;
                NotifyPropertyChanged("TasksDueTomorrowCount");
            }
        }

        private int tasksOverdueCount;
        public int TasksOverdueCount
        {
            get { return tasksOverdueCount; }
            set
            {
                tasksOverdueCount = value;
                NotifyPropertyChanged("TasksOverdueCount");
            }
        }

        private int tasksDoneCount;
        public int TasksDoneCount
        {
            get { return tasksDoneCount; }
            set
            {
                tasksDoneCount = value;
                NotifyPropertyChanged("TasksDoneCount");
            }
        }

        private int tasksNotDoneCount;
        public int TasksNotDoneCount
        {
            get { return tasksNotDoneCount; }
            set
            {
                tasksNotDoneCount = value;
                NotifyPropertyChanged("TasksNotDoneCount");
            }
        }
    }
}
