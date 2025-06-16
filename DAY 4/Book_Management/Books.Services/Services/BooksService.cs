using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.DataAccess.Models;
using Books.DataAccess;
using Books.DataAccess.Repositories;

namespace Books.Services.Services
{
    public class BooksService
    {
        private readonly BooksRepository _booksRepository;

        public BooksService(BooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public List<Book> GetBooks()
        {
            return _booksRepository.GetBooks();
        }

        public Book GetBookById(int id)
        {
            return _booksRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _booksRepository.AddBook(book);
        }

        public int UpdateBook(Book book)
        {
            return _booksRepository.UpdateBook(book);
        }

        public int DeleteBook(int id)
        {
            return _booksRepository.DeleteBook(id);
        }

        public List<Book> GetFilteredBooks(string genre)
        {
            return _booksRepository.GetFilteredBooks(genre);
        }
    }
}
