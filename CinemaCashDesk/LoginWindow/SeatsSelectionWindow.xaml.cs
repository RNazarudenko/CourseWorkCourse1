using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ClassesOfProject;
using Newtonsoft.Json;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for SeatsSelectionWindow.xaml
    /// </summary>
    public partial class SeatsSelectionWindow : Window
    {
        private const int Rows = 5;
        private const int Columns = 10;
        private Button[,] seats;
        private List<Place> places;
        private string saveFilePath = "";
        private Cinema selectedCinema;
        private int selectedTime;
        CinemasWindow _cinemasWindow;

        public SeatsSelectionWindow(CinemasWindow cinemasWindow, Cinema cinema, int time)
        {
            _cinemasWindow = cinemasWindow;
            InitializeComponent();
            selectedCinema = cinema;
            selectedTime = time;
            InitializeSeats();
            saveFilePath = $"occupied{cinema.Title}_{time.ToString()}.json";
            selectedCinema.HallNumber = _cinemasWindow.listBoxMovies.SelectedIndex + 1;
            labelHall.Text += cinema.HallNumber.ToString();
            LoadOccupiedSeats();
        }

        private void InitializeSeats()
        {
            seats = new Button[Rows, Columns];
            places = new List<Place>();

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    var seatButton = new Button
                    {
                        Content = $"{i + 1}-{j + 1}",
                        Background = Brushes.Green,
                        Margin = new Thickness(2)
                    };
                    seatButton.Click += SeatButton_Click;
                    SeatsGrid.Children.Add(seatButton);
                    seats[i, j] = seatButton;
                    selectedCinema.HallNumber = _cinemasWindow.listBoxMovies.SelectedIndex + 1;
                    places.Add(new Place(
                        selectedCinema.Title,
                        selectedCinema.Title,
                        selectedCinema.Year,
                        selectedCinema.OriginalName,
                        selectedCinema.CriticsRating,
                        selectedCinema.Language,
                        selectedCinema.Genre,
                        selectedCinema.Duration,
                        selectedCinema.HallNumber,
                        i + 1,
                        j + 1,
                        "вільно",
                        120.0));
                }
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int row = int.Parse(button.Content.ToString().Split('-')[0]) - 1;
            int number = int.Parse(button.Content.ToString().Split('-')[1]) - 1;
            var place = places.Find(p => p.Row == row + 1 && p.Number == number + 1);

            if (place.Status == "вільно")
            {
                place.Status = "заброньовано";
                button.Background = Brushes.Yellow;
            }
            else if (place.Status == "заброньовано")
            {
                place.Status = "вільно";
                button.Background = Brushes.Green;
            }
            else if (place.Status == "зайнято" && button.IsEnabled)
            {
                place.Status = "вільно";
                button.Background = Brushes.Green;
            }
        }

        private void BookSeats_Click(object sender, RoutedEventArgs e)
        {
            var tickets = LoadExistingTickets();
            int maxTicketID = tickets.Any() ? tickets.Max(t => t.TicketId) : 0;

            foreach (var place in places)
            {
                if (place.Status == "заброньовано")
                {
                    place.Status = "зайнято";
                    var button = seats[place.Row - 1, place.Number - 1];
                    button.Background = Brushes.Red;
                    button.IsEnabled = false;
                    DateTime today = DateTime.Today;
                    var session = (_cinemasWindow.comboBoxTime.SelectedItem as ComboBoxItem)?.Content?.ToString();
                    var ticket = new Ticket(
                        place.Title,
                        place.Age,
                        place.Year,
                        place.OriginalName,
                        place.CriticsRating,
                        place.Language,
                        place.Genre,
                        place.Duration,
                        place.HallNumber,
                        place.Row,
                        place.Number,
                        place.Status,
                        place.Price,
                        DateTime.Today.Date.ToString("d"),
                        session,
                        maxTicketID + 1);

                    tickets.Add(ticket);

                    var ticketWindow = new TicketWindow(ticket);
                    ticketWindow.ShowDialog();
                }
            }
            SaveTickets(tickets);
            SaveOccupiedSeats();
            MessageBox.Show("Місця успішно заброньовані.");
            this.Close();
        }

        private List<Ticket> LoadExistingTickets()
        {
            var filePath = "tickets.json";
            if (File.Exists(filePath))
            {
                var ticketsJson = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson) ?? new List<Ticket>();
            }
            return new List<Ticket>();
        }

        private void SaveTickets(List<Ticket> tickets)
        {
            var filePath = "tickets.json";
            File.WriteAllText(filePath, JsonConvert.SerializeObject(tickets));
        }

        private void ClearSelectedSeats_Click(object sender, RoutedEventArgs e)
        {
            foreach (var place in places)
            {
                if (place.Status == "зайнято")
                {
                    var button = seats[place.Row - 1, place.Number - 1];
                    button.Background = Brushes.Red;
                    button.IsEnabled = true;
                }
                else
                {
                    var button = seats[place.Row - 1, place.Number - 1];
                    button.IsEnabled = false;
                }
            }
        }

        private void ClearAllSeats_Click(object sender, RoutedEventArgs e)
        {
            foreach (var place in places)
            {
                place.Status = "вільно";
                var button = seats[place.Row - 1, place.Number - 1];
                button.Background = Brushes.Green;
                button.IsEnabled = true;
            }
            SaveOccupiedSeats();
            MessageBox.Show("Всі місця успішно очищені.");
            this.Close();
        }

        private void SaveOccupiedSeats()
        {
            var occupiedPlaces = new List<Place>();
            foreach (var place in places)
                if (place.Status == "зайнято")
                    occupiedPlaces.Add(place);
            File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(occupiedPlaces));
        }

        private void LoadOccupiedSeats()
        {
            if (File.Exists(saveFilePath))
            {
                var occupiedPlacesJson = File.ReadAllText(saveFilePath);
                if (!string.IsNullOrEmpty(occupiedPlacesJson))
                {
                    var occupiedPlaces = JsonConvert.DeserializeObject<List<Place>>(occupiedPlacesJson);

                    foreach (var place in occupiedPlaces)
                    {
                        var button = seats[place.Row - 1, place.Number - 1];
                        button.Background = Brushes.Red;
                        button.IsEnabled = false;

                        var existingPlace = places.Find(p => p.Row == place.Row && p.Number == place.Number);
                        if (existingPlace != null)
                            existingPlace.Status = "зайнято";
                    }
                }
            }
        }
    }
}
