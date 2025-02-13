using ClassesOfProject;
using System.Windows;

namespace LoginWindow
{
    public partial class TicketWindow : Window
    {
        public TicketWindow(Ticket ticket)
        {
            InitializeComponent();
            DisplayTicketDetails(ticket);
        }

        private void DisplayTicketDetails(Ticket ticket)
        {
            TicketDetailsTextBlock.Text = $"Фільм: {ticket.Title}\n" +
                                          $"Дата: {ticket.Date}\n" +
                                          $"Сеанс: {ticket.Session}\n" +
                                          $"Зал: {ticket.HallNumber}\n" +
                                          $"Ряд: {ticket.Row}\n" +
                                          $"Місце: {ticket.Number}\n" +
                                          $"Ціна: {ticket.Price.ToString("C")}\n" +
                                          $"Квиток: {ticket.TicketId}";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
