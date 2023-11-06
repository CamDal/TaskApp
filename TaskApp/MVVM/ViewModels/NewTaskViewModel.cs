using System;
using System.Collections.ObjectModel;
using PropertyChanged;
using TaskApp.MVVM.Models;

public class NewTaskViewModel
{
    public event EventHandler<TaskAddedEventArgs> TaskAdded;

    public string Task { get; set; }
    public ObservableCollection<MyTask> Tasks { get; set; }
    public ObservableCollection<Category> Categories { get; set; }
    public Category SelectedCategory { get; set; }

    public NewTaskViewModel()
    {
        //Categories with sample data
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
                Color = "#007BFF"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Shopping",
                Color = "#008000"
            }
        };

        //Tasks with sample data
        Tasks = new ObservableCollection<MyTask>()
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
    }

    public class TaskAddedEventArgs : EventArgs
    {
        public MyTask NewTask { get; }

        public TaskAddedEventArgs(MyTask newTask)
        {
            NewTask = newTask;
        }
    }

    //Method to add tasks
   public void AddTask()
    {
        if (SelectedCategory != null)
        {
            var newTask = new MyTask { TaskName = Task, Completed = false, CategoryId = SelectedCategory.Id };
            SelectedCategory.AddTask(newTask);
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            Task = string.Empty;
        }
    }
}
