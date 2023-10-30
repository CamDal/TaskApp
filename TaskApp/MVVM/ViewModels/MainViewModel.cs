using TaskApp.MVVM.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel()
        {
            FileData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void FileData()
        {
            Categories = new ObservableCollection<Category>()
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "Cleaning",
                    Color = "#ff0000"
                },

                new Category
                {
                    Id = 2,
                    CategoryName = "School/Work",
                    Color = "#7393B3"
                },

                new Category
                {
                    Id = 3,
                    CategoryName = "Shopping",
                    Color = "#008000"
                },

            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Laundry",
                    Completed = false,
                    CategoryId = 1,
                },

                new MyTask
                {
                    TaskName = "Vacuum",
                    Completed = false,
                    CategoryId = 1,
                },

                new MyTask
                {
                    TaskName = "Study for exam",
                    Completed = false,
                    CategoryId = 2,
                },

                new MyTask
                {
                    TaskName = "Finish Website for friend",
                    Completed = false,
                    CategoryId = 2,
                },

                new MyTask
                {
                    TaskName = "Finish Task App",
                    Completed = false,
                    CategoryId = 2,
                },

                new MyTask
                {
                    TaskName = "Buy eggs",
                    Completed = false,
                    CategoryId = 3,
                },

                new MyTask
                {
                    TaskName = "Buy Milk",
                    Completed = false,
                    CategoryId = 3,
                }
            };

            UpdateData();
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                           where t.CategoryId == c.Id
                           select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var noComleted = from t in tasks
                                 where t.Completed == false
                                 select t;

                c.PendingTasks = noComleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor = 
                    (
                        from c in Categories
                        where c.Id == t.CategoryId
                        select c.Color
                     ).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}
