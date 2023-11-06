using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TaskApp.MVVM.Models
{
    //Setting Category Properties
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public int PendingTasks { get; set; }
        public float Percentage { get; set; }
        public bool IsSelected { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        private int totalTasks;

        public int TotalTasks
        {
            get { return totalTasks; }
            set
            {
                if (totalTasks != value)
                {
                    totalTasks = value;
                }
            }
        }

        public Category()
        {
            // Initialize the Tasks collection and update total tasks
            Tasks = new ObservableCollection<MyTask>();
            UpdateTotalTasks();
        }

        // Method to add a task to this category
        public void AddTask(MyTask task)
        {
            Tasks.Add(task);
            UpdateTotalTasks();
        }
        
        // Method to remove a task from this category
        public void RemoveTask(MyTask task)
        {
            Tasks.Remove(task);
            UpdateTotalTasks();
        }

        // Method to update the total number of tasks in this category
        public void UpdateTotalTasks()
        {
            TotalTasks = Tasks.Count;
        }
    }
}
