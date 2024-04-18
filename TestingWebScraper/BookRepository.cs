using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TestingWebScraper
{
    public class BookRepository
    {
        public string? ConnectionString { get; set; }

        public BookRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Create(Book book)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Book (Title, Author, PageCount)" +
                                                 "VALUES(@Title, @Author, @PageCount)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
                cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = book.AuthorAndYear;
                cmd.Parameters.Add("@PageCount", SqlDbType.Int).Value = book.PageCount;
                book.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
