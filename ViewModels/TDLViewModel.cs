using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class TDLViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        private UpdateTDLView view;
        private ToDoListItem selectedItem;
        private ToDoListRepository toDoListRepository;
        private CommandItemType type;
        private ImageSelector imageSelector;

        public ICommand OKCommand { get; set; }

        public ICommand PrevImageCommand { get; set; }
        public ICommand NextImageCommand { get; set; }


        public TDLViewModel(UpdateTDLView view, ToDoListItem selectedItem, ToDoListRepository toDoListRepository, CommandItemType type)
        {
            this.view = view;
            this.selectedItem = selectedItem;
            this.toDoListRepository = toDoListRepository;
            this.type = type;

            imageSelector = new ImageSelector();
            ImgUser = new BitmapImage(new Uri(imageSelector.SelectedImagePath, UriKind.Relative));

            OKCommand = new RelayCommand(Ok);
            PrevImageCommand = new RelayCommand(PrevImage);
            NextImageCommand = new RelayCommand(NextImage);
        }

        private void Ok()
        {
            // force update of binding
            BindingExpression binding = view.txtAnswer.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            var newItem = new ToDoListItem(Answer, imageSelector.SelectedImagePath);
            switch (type)
            {
                case CommandItemType.AddRoot:
                    toDoListRepository.AddRootTDL(newItem);
                    break;
                case CommandItemType.AddSub:
                    if (selectedItem != null)
                        toDoListRepository.AddSubTDL(newItem, selectedItem);
                    break;
                case CommandItemType.Edit:
                    if (selectedItem != null)
                        toDoListRepository.EditTDL(newItem, selectedItem);
                    break;
                default:
                    break;
            }

            CloseAction();
        }

        private void PrevImage()
        {
            imageSelector.SelectPrevious();
            ImgUser = new BitmapImage(new Uri(imageSelector.SelectedImagePath, UriKind.Relative));
        }

        private void NextImage()
        {
            imageSelector.SelectNext();
            ImgUser = new BitmapImage(new Uri(imageSelector.SelectedImagePath, UriKind.Relative));
        }

        private string answer;
        public string Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                NotifyPropertyChanged("Answer");
            }
        }

        private BitmapImage imgUser;
        public BitmapImage ImgUser
        {
            get { return imgUser; }
            set
            {
                imgUser = value;
                NotifyPropertyChanged("ImgUser");
            }
        }

    }
}
