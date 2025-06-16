using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.DataAccess.Models;

namespace Books.DataAccess.Repositories
{
    public class BooksRepository
    {
        private readonly BooksDbContext _bookDbContext;

        public BooksRepository(BooksDbContext booksDbContext)
        {
            _bookDbContext = booksDbContext;
        }

        public List<Book> GetBooks()
        {
            return _bookDbContext.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            Book book = _bookDbContext.Books.FirstOrDefault(book => book.Id == id);
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public void AddBook(Book book)
        {
            _bookDbContext.Books.Add(book);
            _bookDbContext.SaveChanges();
        }

        public int UpdateBook(Book book)
        {
            Book bookToBeUpdated = GetBookById(book.Id);
            if (bookToBeUpdated == null)
            {
                return -1;
            }
            else
            {
                bookToBeUpdated.Title = book.Title;
                bookToBeUpdated.Author = book.Author;
                bookToBeUpdated.Genre = book.Genre;
                _bookDbContext.SaveChanges();
                return 1;
            }
        }

        public int DeleteBook(int id)
        {
            Book bookToBeRemoved = GetBookById(id);
            if (bookToBeRemoved == null)
            {
                return -1;
            }
            else
            {
                _bookDbContext.Books.Remove(bookToBeRemoved);
                _bookDbContext.SaveChanges();
                return 1;
            }
        }

        public List<Book> GetFilteredBooks(string genre)
        {
            List<Book> books = _bookDbContext.Books.Where(book => book.Genre.Equals(genre)).ToList();
            return books;
        }
    }
}
