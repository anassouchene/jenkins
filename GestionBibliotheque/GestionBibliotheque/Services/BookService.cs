using System;
using GestionBibliotheque.DTOs;
using GestionBibliotheque.Exceptions;
using GestionBibliotheque.Mappers;
using GestionBibliotheque.Models;
using GestionBibliotheque.Services.Interfaces;

namespace GestionBibliotheque.Services
{
    /// <summary>
    /// Implémentation du service de gestion des livres.
    /// </summary>

    public class BookService : IBookService
	{
        public IList<Book> books;

		public BookService()
		{
            books = new List<Book>();
		}

        /// <inheritdoc/>
        
        public Book AddBook(BookAddDto bookModel)
        {
            if (bookModel == null) throw new BookNotFoundException("Book Not Found !");
            Book book = BookMapper.BookAddDtoToBook(bookModel);
            if (BookExist(book.ISBN))
            {
                throw new DuplicateISBNException("The book's ISBN already exists !");
            }
            books.Add(book);
            return book;  
        }

        /// <inheritdoc/>
        public void DeleteBook(int id)
        {
            Book book = GetBookById(id) ?? throw new BookNotFoundException("Book Not Found !");
            books.Remove(book);
        }

        /// <inheritdoc/>
        public IList<BookDto> GetAllBooks()
        {
            return BookMapper.BookToBookDto(this.books);
        }

        /// <inheritdoc/>
        public void UpdateBook(int id , BookUpdateDto bookModel)
        {
            if(id != bookModel.Id)
            {
                throw new InvalidIdException("Invalid Id !");
            }
            if (BookExist(bookModel.ISBN))
            {
                throw new DuplicateISBNException("ce ISBN est deja existe");
            }
            Book book = GetBookById(id);
            book.Author = bookModel.Author;
            book.Copies = bookModel.Copies;
            book.ISBN = bookModel.ISBN;
            book.Price = bookModel.Price;
            book.Title = bookModel.Title;
            book.Year = bookModel.Year;
        }

        /// <inheritdoc/>
        public BookDto GetBookByIdAndTitle(BookFilterDto filterDto)
        {
            Book book = books.FirstOrDefault(b => b.IdBook == filterDto.Id && b.Title.Equals(filterDto.Title, StringComparison.OrdinalIgnoreCase)) ?? throw new BookNotFoundException("Aucun livre trouvé !.");

            return BookMapper.BookToBookDto(book);
        }

        private Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.IdBook == id) ?? throw new BookNotFoundException("Book Not Found !");
        }

        private bool BookExist(string ISBN)
        {
            return books.Any(b => b.ISBN == ISBN);
        }
    }
}

