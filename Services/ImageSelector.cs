using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public class ImageSelector
    {
        private string[] imagePaths = { "/Resources/folder.png", "/Resources/books.png", "/Resources/discord.png", 
            "/Resources/contacts.png", "/Resources/code.png", "/Resources/computer.png", "/Resources/home.png", 
            "/Resources/language.png", "/Resources/work.png" };
        private int currentIndex = 0;

        public string SelectedImagePath
        {
            get { return imagePaths[currentIndex]; }
        }

        public void SelectPrevious()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = imagePaths.Length - 1;
            }
        }

        public void SelectNext()
        {
            currentIndex++;
            if (currentIndex >= imagePaths.Length)
            {
                currentIndex = 0;
            }
        }
    }
}
