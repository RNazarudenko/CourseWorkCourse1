using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOfProject
{
    public class Place:Cinema
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public Place(string title, string age, int year, string originalName, double rating, string language, string genre, int duration, int hallNumber, int row, int number, string status, double price):base(title, age, year, originalName, rating,language, genre, duration,hallNumber)
        {
            Row = row;
            Number = number;
            Status = status;
            Price = price;
        }
    }
}