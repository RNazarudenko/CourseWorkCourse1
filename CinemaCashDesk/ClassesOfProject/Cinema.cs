using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesOfProject
{
    public class Cinema
    {
        public string Title { get; set; }
        public string Age { get; set; }
        public int Year { get; set; }
        public string OriginalName { get; set; }
        public double CriticsRating { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public int HallNumber { get; set; }

        public Cinema(string title, string age, int year, string originalName, double rating, string language, string genre, int duration,int hallNumber)
        {
            Title = title;
            Age = age;
            Year = year;
            OriginalName = originalName;
            CriticsRating = rating;
            Language = language;
            Genre = genre;
            Duration = duration;
            HallNumber = hallNumber;
        }
    }
}