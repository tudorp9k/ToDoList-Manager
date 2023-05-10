using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Views;

namespace ToDoList.Services
{
    public class FileMenuCommands : ViewModelBase
    {
        private ToDoListRepository toDoListRepository;
        public ToDoListItem SelectedItem { get; set; }
        public TaskItem SelectedTask { get; set; }

        public ICommand AddRootTDLCommand { get; set; }
        public ICommand AddSubTDLCommand { get; set; }
        public ICommand EditTDLCommand { get; set; }
        public ICommand DeleteTDLCommand { get; set; }
        public ICommand MoveUpTDLCommand { get; set; }
        public ICommand MoveDownTDLCommand { get; set; }
        public ICommand MoveUpSubTDLCommand { get; set; }
        public ICommand MoveDownSubTDLCommand { get; set; }


        public ICommand AddTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand MoveUpTaskCommand { get; set; }
        public ICommand MoveDownTaskCommand { get; set; }
        public ICommand SetTaskDoneCommand { get; set; }
        public ICommand FindTaskCommand { get; set; }
        
        public ICommand SortCommand { get; set; }
        public ICommand FiltersCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }

        public FileMenuCommands(ToDoListRepository toDoListRepository)
        {
            this.toDoListRepository = toDoListRepository;

            AddRootTDLCommand = new RelayCommand(AddRootTDL);
            AddSubTDLCommand = new RelayCommand(AddSubTDL);
            EditTDLCommand = new RelayCommand(EditTDL);
            DeleteTDLCommand = new RelayCommand(DeleteTDL);
            MoveUpTDLCommand = new RelayCommand(MoveUpTDL);
            MoveDownTDLCommand = new RelayCommand(MoveDownTDL);
            MoveUpSubTDLCommand = new RelayCommand(MoveUpSubTDL);
            MoveDownSubTDLCommand = new RelayCommand(MoveDownSubTDL);


            AddTaskCommand = new RelayCommand(AddTask);
            EditTaskCommand = new RelayCommand(EditTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            MoveUpTaskCommand = new RelayCommand(MoveUpTask);
            MoveDownTaskCommand = new RelayCommand(MoveDownTask);
            SetTaskDoneCommand = new RelayCommand(SetTaskDone);
            FindTaskCommand = new RelayCommand(FindTask);

            SortCommand = new RelayCommand(Sort);
            FiltersCommand = new RelayCommand(Filters);
            StatisticsCommand = new RelayCommand(ShowStatistics);
        }

        private void Sort()
        {
            SortTaskView sortTaskView = new SortTaskView(toDoListRepository.GetAll());
            sortTaskView.Show();
        }

        private void Filters()
        {
            FilterTaskView filterTaskView = new FilterTaskView(toDoListRepository.GetAll());
            filterTaskView.Show();
        }

        private void FindTask()
        {
            FindTaskView findTaskView = new FindTaskView(toDoListRepository.GetAll());
            findTaskView.Show();
        }

        private void MoveDownTask()
        {
            toDoListRepository.MoveDownTask(SelectedTask, SelectedItem);
        }

        private void MoveUpTask()
        {
            toDoListRepository.MoveUpTask(SelectedTask, SelectedItem);
        }

        private void DeleteTask()
        {
            toDoListRepository.DeleteTask(SelectedTask, SelectedItem);
        }

        private void EditTask()
        {
            UpdateTaskView updateTaskView = new UpdateTaskView(SelectedTask, SelectedItem, toDoListRepository, CommandItemType.Edit);
            updateTaskView.Show();
        }

        private void AddTask()
        {
            UpdateTaskView updateTaskView = new UpdateTaskView(SelectedTask, SelectedItem, toDoListRepository, CommandItemType.AddTask);
            updateTaskView.Show();
        }

        private void SetTaskDone()
        {
            toDoListRepository.SetTaskDone(SelectedTask, SelectedItem);
        }

        private void MoveUpSubTDL()
        {
            toDoListRepository.MoveUpSubTDL(SelectedItem);
        }

        private void MoveDownSubTDL()
        {
            toDoListRepository.MoveDownSubTDL(SelectedItem);
        }

        private void MoveUpTDL()
        {
            toDoListRepository.MoveUpTDL(SelectedItem);
        }

        private void MoveDownTDL()
        {
            toDoListRepository.MoveDownTDL(SelectedItem);
        }

        private void DeleteTDL()
        {
            toDoListRepository.DeleteTDL(SelectedItem);
        }

        private void EditTDL()
        {
            UpdateTDLView updateTDLView = new UpdateTDLView(SelectedItem, toDoListRepository, CommandItemType.Edit);
            updateTDLView.Show();
        }

        private void AddSubTDL()
        {
            UpdateTDLView updateTDLView = new UpdateTDLView(SelectedItem, toDoListRepository, CommandItemType.AddSub);
            updateTDLView.Show();
        }

        private void AddRootTDL()
        {
            UpdateTDLView updateTDLView = new UpdateTDLView(SelectedItem, toDoListRepository, CommandItemType.AddRoot);
            updateTDLView.Show();
        }

        private void ShowStatistics()
        {
            StatisticsView statisticsView = new StatisticsView(toDoListRepository.GetAll());
            statisticsView.Show();
        }
    }
}
