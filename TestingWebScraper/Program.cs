using HtmlAgilityPack;

namespace TestingWebScraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Write your search term: ");
            string searchTermRaw = Console.ReadLine();
            string searchTerm = "";

            foreach (char c in searchTermRaw)
            {
                if (c == ' ')
                    searchTerm += "%20";
                else
                    searchTerm += c;
            }

            Console.Clear();

            string url = $"https://www.odensebib.dk/search/ting/{searchTerm}?profile=boeger";
            string htmlContent;

            using (HttpClient client = new HttpClient())
            {
                htmlContent = await client.GetStringAsync(url);
            }

            var document = new HtmlDocument();          // This requires HTMLAgilityPack
            document.LoadHtml(htmlContent);

            // Now the HTML is loaded into the variable "document". 

            // Now we load all titles of books into a list. 
            var titleNodes = document.DocumentNode.SelectNodes("//div[@class='field field-name-ting-title field-type-ting-title field-label-hidden']");
            List<string> titles = new List<string>();

            if (titleNodes != null)
            {
                foreach (var titleNode in titleNodes)
                {
                    titles.Add(titleNode.InnerText);
                }
            }

            // Now loading all the authors' names. 

            var authorNodes = document.DocumentNode.SelectNodes("//div[@class='field field-name-ting-author field-type-ting-author field-label-hidden']");
            List<string> authors = new List<string>();

            if (authorNodes != null)
            {
                foreach (var authorNode in authorNodes)
                {
                    authors.Add(authorNode.InnerText);
                }
            }

            // Now loading the page count of all the books.

            var pagecountNodes = document.DocumentNode.SelectNodes("//div[@class='field field-name-ting-details-extent field-type-ting-details-extent field-label-above']");
            List<string> pagecounts = new List<string>();

            if (pagecountNodes != null)
            {
                foreach (var pagecountNode in pagecountNodes)
                {
                    string pageString = pagecountNode.InnerText.Replace("\n", "").Replace("Omfang:&nbsp;", "").Trim();
                    pagecounts.Add(pageString);
                }
            }

            // Now printing it nicely. 

            if (titles.Count == 0)
            {
                Console.WriteLine("No titles found.");
            }

            for (int i = 0; i < titles.Count; i++)
            {
                Console.WriteLine($"Title: {titles[i]}\n{authors[i]}\n{pagecounts[i]}\n");
            }

            // Converting pagecount to int

            List<int> pageCountsReal = new List<int>();

            for (int i = 0; i < pagecounts.Count; i++)
            {
                string number = "";
                foreach (char c in pagecounts[i])
                {
                    if (int.TryParse(c.ToString(), out int k) == true)
                    {
                        number += c;
                    }
                }
                pageCountsReal.Add(int.Parse(number));
            }

            // Now adding to database

            List<Book> books = new List<Book>();

            for (int i = 0; i < titles.Count; ++i)
            {
                books.Add(new Book(titles[i], authors[i], pageCountsReal[i]));
            }

            BookRepository bookRepository = new BookRepository();

            foreach (Book book in books)
            {
                bookRepository.Create(book);
            }

            Console.ReadKey();
        }
    }
}
