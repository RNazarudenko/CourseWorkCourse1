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

        private void ListBoxMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxMovies.SelectedIndex != -1)
            {
                Cinema selectedCinema = cinemas[listBoxMovies.SelectedIndex];
                MovieTitleText.Text = "Назва: " + selectedCinema.Title;
                MovieGenreText.Text = "Жанр: " + selectedCinema.Genre;
                MovieRatingText.Text = "Рейтинг: " + selectedCinema.CriticsRating.ToString();
                MovieYearText.Text = "Рік випуску: " + selectedCinema.Year.ToString();
                MovieAgeText.Text = "Вік: " + selectedCinema.Age.ToString();
                MovieOriginalNameText.Text = "Оригінальна назва: " + selectedCinema.OriginalName;
                MovieLanguageText.Text = "Мова: " + selectedCinema.Language;
                MovieDurationText.Text = "Тривалість: " + selectedCinema.Duration.ToString() + " хв.";
                ChooseSeatsButton.IsEnabled = true;
                comboBoxTime.IsEnabled = true;
                MovieDelateButton.IsEnabled = true;
                SelectedFilm = listBoxMovies.SelectedItem.ToString();
            }
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

        private void ClearMovieDetails()
        {
            MovieTitleText.Text = "";
            MovieGenreText.Text = "";
            MovieRatingText.Text = "";
            MovieYearText.Text = "";
            MovieAgeText.Text = "";
            MovieOriginalNameText.Text = "";
            MovieLanguageText.Text = "";
            MovieDurationText.Text = "";
        }

        private void SaveCinemas()
        {
            File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(cinemas));
        }

        private void LoadCinemas()
        {
            if (File.Exists(saveFilePath))
            {
                var jsonData = File.ReadAllText(saveFilePath);
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    cinemas = JsonConvert.DeserializeObject<List<Cinema>>(jsonData) ?? new List<Cinema>();
                }
            }
        }

        private void RelogProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _mainWindow.Show();
        }
    }
}