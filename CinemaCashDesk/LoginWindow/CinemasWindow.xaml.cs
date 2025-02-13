using ClassesOfProject;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LoginWindow
{
    public partial class CinemasWindow : Window
    {
        private List<Cinema> cinemas;
        private string saveFilePath = "cinemas.json";
        private MainWindow _mainWindow;
        private string SelectedFilm = "";
        public CinemasWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            cinemas = new List<Cinema>();
            _mainWindow = mainWindow;
            LoadCinemas();
        }

        private void MovieAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxMovies.Items.Count < 6)
            {
                MovieAddWindow movieAddWindow = new MovieAddWindow(this);
                movieAddWindow.ShowDialog();
            }
            else
                MessageBox.Show("В списку є максимально фільмів по кількості залів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void AddMovieToList(Cinema cinema)
        {
            cinemas.Add(cinema);
            listBoxMovies.Items.Add(cinema.Title);
            SaveCinemas();
        }

        private void ChooseSeatsButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTime.SelectedIndex >= 0 && comboBoxTime.SelectedIndex <= 6)
            {
                Cinema selectedCinema = cinemas[listBoxMovies.SelectedIndex];
                int selectedTime = comboBoxTime.SelectedIndex + 1;
                SeatsSelectionWindow seatsSelectionWindow = new SeatsSelectionWindow(this,selectedCinema, selectedTime);
                seatsSelectionWindow.ShowDialog();
            }
            else
                MessageBox.Show("Виберіть час сеансу фільма.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MovieDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxMovies.SelectedIndex != -1)
            {
                int selectedIndex = listBoxMovies.SelectedIndex;
                cinemas.RemoveAt(selectedIndex);
                listBoxMovies.Items.RemoveAt(selectedIndex);
                SaveCinemas();
                ClearMovieDetails();
                for (int i = 1; i <= 7; i++)
                {
                    string fileName = $"occupied{SelectedFilm}_{i}.json";
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
                ChooseSeatsButton.IsEnabled = false;
                comboBoxTime.IsEnabled = false;
            }
            else
                MessageBox.Show("Виберіть фільм для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SaveCinemas()
        {
            File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(cinemas));
        }

        private void LoadCinemas()
        {
            if (File.Exists(saveFilePath))
            {
                cinemas = JsonConvert.DeserializeObject<List<Cinema>>(File.ReadAllText(saveFilePath));
                foreach (var cinema in cinemas)
                    listBoxMovies.Items.Add(cinema.Title);
            }
        }

        private void RelogProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _mainWindow.Show();
        }

        private void DisplayMovieDetails(Cinema cinema)
        {
            MovieTitleText.Text = "Назва: " + cinema.Title;
            MovieGenreText.Text = "Жанр: " + cinema.Genre;
            MovieRatingText.Text = "Рейтинг: " + cinema.CriticsRating.ToString();
            MovieYearText.Text = "Рік випуску: " + cinema.Year.ToString();
            MovieAgeText.Text = "Вік: " + cinema.Age.ToString();
            MovieOriginalNameText.Text = "Оригінальна назва: " + cinema.OriginalName;
            MovieLanguageText.Text = "Мова: " + cinema.Language;
            MovieDurationText.Text = "Тривалість: " + cinema.Duration.ToString() + " хв.";
        }

        private void ListBoxMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxMovies.SelectedIndex != -1)
            {
                Cinema selectedCinema = cinemas[listBoxMovies.SelectedIndex];
                DisplayMovieDetails(selectedCinema);
                ChooseSeatsButton.IsEnabled = true;
                comboBoxTime.IsEnabled = true;
                MovieDelateButton.IsEnabled = true;
                SelectedFilm = selectedCinema.Title;
            }
        }

        private void ClearMovieDetails()
        {
            DisplayMovieDetails(new Cinema());
        }
    }
}