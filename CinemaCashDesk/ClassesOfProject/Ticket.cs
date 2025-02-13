using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOfProject
{
    public class Ticket: Place 
    {
        public string Date { get; set; }
        public string Session { get; set; }
        public int TicketId { get; set; }
        public Ticket(string title, string age, int year, string originalName, double rating, string language, string genre, int duration, int hallNumber, int row, int number, string status, double price,string date,string session,int ticketID):base(title, age, year, originalName, rating, language, genre, duration,hallNumber, row,number,status,price)
        {
            Date = date;
            Session = session;
            TicketId = ticketID;
        }
    }
}