using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToDoListContext _context = new ToDoListContext();
        private Task editingTask;


        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            lvTasks.ItemsSource = _context.Tasks.ToList();
        }

        private void txtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void AddTask()
        {
            string taskName = txtNewTask.Text.Trim();
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                var task = new Task { Name = taskName };
                _context.Tasks.Add(task);
                _context.SaveChanges();
                txtNewTask.Clear();
                LoadTasks();
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var taskToDelete = (sender as Button)?.DataContext as Task;
            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                _context.SaveChanges();
                LoadTasks();
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var task = (sender as CheckBox)?.DataContext as Task;
            if (task != null)
            {
                task.IsDone = true;
                _context.SaveChanges();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var task = (sender as CheckBox)?.DataContext as Task;
            if (task != null)
            {
                task.IsDone = false;
                _context.SaveChanges();
            }
        }
        private void lvTasks_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = lvTasks.SelectedItem;
            if (item != null)
            {
                editingTask = item as Task;
                var editTaskWindow = new EditTaskWindow(editingTask);
                editTaskWindow.ShowDialog();
                lvTasks.Items.Refresh(); // Обновить список задач после редактирования
            }
        }
        private void lvTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = lvTasks.SelectedItem;
            if (item != null)
            {
                var task = item as Task;
                if (task != null)
                {
                    var editTaskWindow = new EditTaskWindow(task);
                    if (editTaskWindow.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        LoadTasks();
                    }
                }
            }
        }
    }
}
