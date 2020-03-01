using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }

        public Event(int id, DateTime start, DateTime end, string text, string color)
        {
            this.Id = id;
            this.Start = start;
            this.End = end;
            this.Text = text;
            this.Color = color;
        }
    }

    public class CalendarContext : DbContext
    {
        //public DbSet<Event> Events { get; set; }

        public DbSet<Seance> Seances { get; set; }

        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options) { }
    }
}
