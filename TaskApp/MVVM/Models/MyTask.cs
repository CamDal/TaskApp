using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.MVVM.Models
{
    //Setting MyTask Properties
    [AddINotifyPropertyChangedInterface]
    public class MyTask
    {
        public string TaskName { get; set; }
        public bool Completed { get; set; }
        public int CategoryId { get; set; }
        public string TaskColor { get; set; }
        public DateTime FinishDate { get; set; }
        public bool reminder { get; set; }
    }
}
