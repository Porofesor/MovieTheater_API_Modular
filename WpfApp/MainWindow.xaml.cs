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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> Data = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            Data.Add(new Person { FirstName= "Tim", LastName="Corey"});
            Data.Add(new Person { FirstName = "Jim", LastName = "Romero" });
            Data.Add(new Person { FirstName = "Nick", LastName = "Chapas" });
            
            myComboBox.ItemsSource = Data;
        }


        private void submitButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string FullName
            {
                get
                {
                    return $"{FirstName} {LastName}";
                }
            }
        }
    }
}