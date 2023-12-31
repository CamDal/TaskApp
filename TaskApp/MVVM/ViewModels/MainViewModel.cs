﻿using System.Collections.ObjectModel;
using TaskApp.MVVM.Models;
using PropertyChanged; // Assuming this is used for property change notification
using System.Collections.Specialized;
using System.Linq;

namespace TaskApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel()
        {
            // Initialize data and event handlers
            FileData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            // Initialize the Tasks collection within each Category
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<MyTask>(Tasks.Where(task => task.CategoryId == category.Id));
                category.UpdateTotalTasks();
            }
        }

        // Event handler for changes in the Tasks collection
        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Handle tasks collection changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MyTask task in e.NewItems)
                {
                    // Find the category associated with the added task
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                    if (category != null)
                    {
                        // Update category data based on the added task
                        category.Tasks.Add(task);
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MyTask task in e.OldItems)
                {
                    // Find the category associated with the removed task
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                    if (category != null)
                    {
                        // Update category data based on the removed task
                        category.Tasks.Remove(task);
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
        }

        // Initialize Categories and Tasks with hardcoded data
        private void FileData()
        {
            Categories = new ObservableCollection<Category>()
            {
                // Define categories with sample data
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
                    Color = "#007BFF"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Shopping",
                    Color = "#008000"
                }
            };

            Tasks = new ObservableCollection<MyTask>()
            {
                // Define tasks with sample data
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

        // Update data for categories and tasks
        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                // Filter tasks for the current category
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                // Count completed and non-completed tasks
                var completed = from t in tasks
                                where t.Completed == true
                                select t;
                var noCompleted = from t in tasks
                                  where t.Completed == false
                                  select t;

                // Update category properties
                c.PendingTasks = noCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                // Find the category color for the task
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
