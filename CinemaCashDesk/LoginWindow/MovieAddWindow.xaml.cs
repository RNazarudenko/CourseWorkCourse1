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
using System.Windows.Shapes;
using ClassesOfProject;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MovieAddWindow.xaml
    /// </summary>
    public partial class MovieAddWindow : Window
    {
        private CinemasWindow _cinemasWindow;

        public MovieAddWindow(CinemasWindow cinemasWindow)
        {
            InitializeComponent();
            _cinemasWindow = cinemasWindow;
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            Cinema cinema = new Cinema(TitleTextBox.Text, AgeTextBox.Text, int.Parse(YearTextBox.Text), OriginalNameTextBox.Text, double.Parse(RatingTextBox.Text),LanguageTextBox.Text, GenreTextBox.Text,int.Parse(DurationTextBox.Text),(_cinemasWindow.listBoxMovies.SelectedIndex+1));
            _cinemasWindow.AddMovieToList(cinema);
            this.Close();
        }
    }
}