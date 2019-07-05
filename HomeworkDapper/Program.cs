using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HomeworkDapper
{
    class Program
    {
        private static string _connectionString = @"Server=DESKTOP-I98ASMM\SQLEXPRESS;Database=AdoNetDB;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            //AddAuthor();
            //AddBook();
            //Console.WriteLine("Enter id");
            //string stringId = Console.ReadLine();
            //int id = int.Parse(stringId);
            //EditAuthor(id);
            //EditBook(id);
            GetAllAuthorsWithBooks();
            
        }
        private static void AddAuthor()
        {
            Console.WriteLine("Enter author first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter author last name:");
            string lasttName = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var newPizza = connection.Execute("AddAuthor", new { FirstName = firstName, LastName = lasttName }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        private static void AddBook()
        {
            Console.WriteLine("Enter book title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter genre:");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter author id:");
            string id = Console.ReadLine();
            int authorId = int.Parse(id);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var newPizza = connection.Execute("AddBook", new { Title = title, Genre = genre,AuthorId = authorId }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        private static void EditAuthor(int id)
        {
            Console.WriteLine("Enter author first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter author last name:");
            string lasttName = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var newPizza = connection.Execute("EditAuthor", new {Id = id, FirstName = firstName, LastName = lasttName }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        private static void EditBook(int id)
        {
            Console.WriteLine("Enter book title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter genre:");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter author id:");
            string stringId = Console.ReadLine();
            int authorId = int.Parse(stringId);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var newPizza = connection.Execute("EditBook", new { Id = id,Title = title, Genre = genre, AuthorId = authorId }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        private static void GetAllAuthorsWithBooks()
        {
            string query = "Select * from Book as b inner join Author as a on a.Id = b.AuthorId";
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var booksDictionary = new Dictionary<int, Book>();
                var books = connection.Query<Book, Author, Book>(query,
                    (book, author) =>
                    {
                        book.Author = author;
                        return book;
                    },
                    splitOn: "AuthorId")
                    .Distinct().ToList();
                foreach(var book in books)
                {
                    Console.WriteLine($"{book.Author.FirstName} {book.Author.LastName} - {book.Title}");
                }
                Console.ReadLine();
            }
        }
    }
}
