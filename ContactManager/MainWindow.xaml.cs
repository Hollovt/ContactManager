using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using Contract;
namespace ContactManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// public class YesNoToBooleanConverter : IValueConverter
    
    
    public partial class MainWindow : Window
    {
        ObservableCollection<Contact> contacts ;
        List<IValidation> validationRules;
        private void SetBindings(AddContactWindow Add_Winndow)
        {
            if (NameValidatonComboBox.SelectedItem != null)
            {
                Binding binding = BindingOperations.GetBinding(Add_Winndow.NameTextBox, TextBox.TextProperty);
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add((ValidationRule)NameValidatonComboBox.SelectedItem);
            }
            if (SurnameValidatonComboBox.SelectedItem != null)
            {
                Binding binding = BindingOperations.GetBinding(Add_Winndow.SurnameTextBox, TextBox.TextProperty);
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add((ValidationRule)SurnameValidatonComboBox.SelectedItem);
            }
            if (PhoneValidatonComboBox.SelectedItem != null)
            {
                Binding binding = BindingOperations.GetBinding(Add_Winndow.PhoneTextBox, TextBox.TextProperty);
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add((ValidationRule)PhoneValidatonComboBox.SelectedItem);
            }
            if (EmailValidatonComboBox.SelectedItem != null)
            {
                Binding binding = BindingOperations.GetBinding(Add_Winndow.EmailTextBox, TextBox.TextProperty);
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add((ValidationRule)EmailValidatonComboBox.SelectedItem);
            }
        }
        private void ReadLibraries()
        {
            
            validationRules = new List<IValidation>();

            string[] fs = Directory.GetFiles(".\\Validation", "*.dll");
            foreach (string f in fs)
            {
                Assembly a = Assembly.Load(File.ReadAllBytes(f));
                foreach (Type t in a.GetTypes())
                {
                    if (t.GetInterfaces().Contains(typeof(IValidation)))
                    {
                        IValidation vr = (IValidation)Activator.CreateInstance(t);

                        validationRules.Add(vr);

                    }
                }
            }
            ValidationSetings.DataContext = validationRules;
        }
        //ObservableCollection<Contact> DataGridContacts;
        public void AddContact(string name, string surname, string mail, string phone,Contact.GenderType gender)
        {
            contacts.Add(new Contact(name, surname, mail, phone, gender));
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
           
           
            MessageBox.Show("This is simple contact manager", "Contact Mangager", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            contacts = new ObservableCollection<Contact>();
            this.DataContext = contacts;
            List.SelectionMode = SelectionMode.Single;
            ReadLibraries();
          
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            contacts = new ObservableCollection<Contact>();
            this.DataContext = contacts;
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {   
            var Add_Winndow = new AddContactWindow();
            SetBindings(Add_Winndow);
            var blurWindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Height = this.Height,
                Width = this.Width-10,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
                
            
            blurWindow.Show();
            

            Add_Winndow.ShowDialog();
            if(Add_Winndow.is_added)
            {
                string name = Add_Winndow.NameTextBox.Text;
                string surname = Add_Winndow.SurnameTextBox.Text;
                string email = Add_Winndow.EmailTextBox.Text;
                string phone = Add_Winndow.PhoneTextBox.Text;
                if (Add_Winndow.GenderComboBox.SelectedIndex == 0)
                    contacts.Add(new Contact(name, surname, email, phone, Contact.GenderType.male));
                else
                    contacts.Add(new Contact(name, surname, email, phone, Contact.GenderType.female));

            }
            blurWindow.Owner = null;
            blurWindow.Close();
            
        }

        private void ExportContacts_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string FilePath="";
            saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;

                XmlSerializer x = new XmlSerializer(typeof(ObservableCollection<Contact>));
                TextWriter writer = new StreamWriter(FilePath);
                x.Serialize(writer, contacts);
                writer.Close();
            }
        }

        private void ImprtContacts_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string FilePath = "";
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
           
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Contact>));
                using (Stream reader = new FileStream(FilePath, FileMode.Open))
                {
                    contacts = (ObservableCollection<Contact>)serializer.Deserialize(reader);
                }
                this.DataContext = contacts;
            }
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValidationButton.IsChecked = false;
        }


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (List.SelectedIndex == -1) return;

          
            contacts.RemoveAt(List.SelectedIndex);

           
            
        }
    }
    
}
