using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public string StatusDescription
        {
            get { return IsDone ? "Выполнено" : "Не выполнено"; }
        }
    }
}

