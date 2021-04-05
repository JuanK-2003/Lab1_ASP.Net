using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1_WebForm.Class
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime YaerOfPublication { get; set; }

        public Book()
        {

        }

        public Book(string id, string title, string author, DateTime yaerOfPublication)
        {
            Id = id;
            Title = title;
            Author = author;
            YaerOfPublication = yaerOfPublication;
        }

        public Book(string Id, string Title = "")
        {
            this.Id = Id;
            this.Title = Title;
        }
    }
}