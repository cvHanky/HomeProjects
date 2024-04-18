using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWebScraper
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorAndYear { get; set; }
        public int PageCount { get; set; }

        public Book(string title, string authorAndYear, int pageCount)
        {
            Title = title;
            AuthorAndYear = authorAndYear;
            PageCount = pageCount;
        }


    }
}
