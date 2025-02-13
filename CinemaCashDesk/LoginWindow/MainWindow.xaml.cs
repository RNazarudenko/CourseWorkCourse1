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
using System.Xml;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button1.Content.ToString();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button2.Content.ToString();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button3.Content.ToString();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button4.Content.ToString();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button5.Content.ToString();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button6.Content.ToString();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button7.Content.ToString();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button8.Content.ToString();
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button9.Content.ToString();
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox1.Password.Length < PasswordBox1.MaxLength)
                PasswordBox1.Password += Button0.Content.ToString();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox1.Clear();
        }

        private void PasswordBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void PasswordBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string[] passwords = { "1111", "2222", "3333", "4444", "5555", "6666", "7777", "8888", "9999", "0000" };
            string[] accounts = { "Єкатерина", "Анастасія", "Вікторія", "Дар'я", "Дмитро", "Юрій", "Юлія", "Марія", "Валерія", "Назарій" };
            if (passwords.Contains(PasswordBox1.Password))
            {
                for (int i = 0; i < passwords.Length; i++)
                    if (passwords[i] == PasswordBox1.Password)
                    {
                        this.Hide();
                        CinemasWindow cinemasWindow = new CinemasWindow(this);
                        cinemasWindow.Show();
                        if (cinemasWindow.WindowState == WindowState.Normal)
                        {
                            cinemasWindow.WindowState = WindowState.Maximized;
                            cinemasWindow.WindowStyle = WindowStyle.None;
                        }
                        else
                        {
                            cinemasWindow.WindowState = WindowState.Normal;
                            cinemasWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                        }
                        cinemasWindow.labelAccount.Content = accounts[i];
                    }
            }
            else
                MessageBox.Show("Немає користувача під таким паролем", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExitProgramButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}