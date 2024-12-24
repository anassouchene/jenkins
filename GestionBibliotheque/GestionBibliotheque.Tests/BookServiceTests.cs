using System;
using FluentAssertions;
using GestionBibliotheque.DTOs;
using GestionBibliotheque.Exceptions;
using GestionBibliotheque.Models;
using GestionBibliotheque.Services;

public class BookServiceTests
{
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        // Initialiser le service avec une liste vide
        _bookService = new BookService();
    }

    [Fact]
    public void AddBook_Should_Add_Book_When_Valid()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };

        // Act
        var result = _bookService.AddBook(bookDto);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(bookDto.Title);
        _bookService.books.Count.Should().Be(1);
    }

    [Fact]
    public void AddBook_Should_Throw_DuplicateISBNException_When_ISBN_Already_Exists()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };
        _bookService.AddBook(bookDto);

        // Act
        Action act = () => _bookService.AddBook(bookDto);

        // Assert
        act.Should().Throw<DuplicateISBNException>()
            .WithMessage("The book's ISBN already exists !");
    }

    [Fact]
    public void AddBook_Should_Throw_BookNotFoundException_When_Null()
    {
        // Act
        Action act = () => _bookService.AddBook(null);

        // Assert
        act.Should().Throw<BookNotFoundException>()
            .WithMessage("Book Not Found !");
    }

    [Fact]
    public void DeleteBook_Should_Remove_Book_When_Valid_Id()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };
        var book = _bookService.AddBook(bookDto);

        // Act
        _bookService.DeleteBook(book.IdBook);

        // Assert
        _bookService.books.Should().BeEmpty();
    }

    [Fact]
    public void DeleteBook_Should_Throw_BookNotFoundException_When_Id_Does_Not_Exist()
    {
        // Act
        Action act = () => _bookService.DeleteBook(999);

        // Assert
        act.Should().Throw<BookNotFoundException>()
            .WithMessage("Book Not Found !");
    }

    [Fact]
    public void UpdateBook_Should_Update_Book_When_Valid()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };
        var book = _bookService.AddBook(bookDto);

        var updateDto = new BookUpdateDto
        {
            Id = book.IdBook,
            Title = "The Clean Coder",
            Author = "Robert C. Martin",
            ISBN = "9780137081073",
            Price = 40.0m,
            Year = 2011,
            Copies = 5
        };

        // Act
        _bookService.UpdateBook(book.IdBook, updateDto);

        // Assert
        var updatedBook = _bookService.books.First();
        updatedBook.Title.Should().Be(updateDto.Title);
        updatedBook.Price.Should().Be(updateDto.Price);
        updatedBook.Year.Should().Be(updateDto.Year);
        updatedBook.Copies.Should().Be(updateDto.Copies);
        updatedBook.ISBN.Should().Be(updateDto.ISBN);
    }

    [Fact]
    public void UpdateBook_Should_Throw_InvalidIdException_When_Id_Does_Not_Match()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };
        var book = _bookService.AddBook(bookDto);

        var updateDto = new BookUpdateDto
        {
            Id = 999, // Faux ID
            Title = "The Clean Coder",
            Author = "Robert C. Martin",
            ISBN = "9780137081073",
            Price = 40.0m,
            Year = 2011,
            Copies = 5
        };

        // Act
        Action act = () => _bookService.UpdateBook(book.IdBook, updateDto);

        // Assert
        act.Should().Throw<InvalidIdException>()
            .WithMessage("Invalid Id !");
    }

    [Fact]
    public void GetBookByIdAndTitle_Should_Return_Book_When_Valid_Filter()
    {
        // Arrange
        var bookDto = new BookAddDto
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "9780132350884",
            Price = 30.5m,
            Year = 2008,
            Copies = 10
        };
        var book = _bookService.AddBook(bookDto);

        var filterDto = new BookFilterDto
        {
            Id = book.IdBook,
            Title = "Clean Code"
        };

        // Act
        var result = _bookService.GetBookByIdAndTitle(filterDto);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(book.Title);
    }

    [Fact]
    public void GetBookByIdAndTitle_Should_Throw_BookNotFoundException_When_Filter_Invalid()
    {
        // Arrange
        var filterDto = new BookFilterDto
        {
            Id = 999,
            Title = "Nonexistent Book"
        };

        // Act
        Action act = () => _bookService.GetBookByIdAndTitle(filterDto);

        // Assert
        act.Should().Throw<BookNotFoundException>()
            .WithMessage("Aucun livre trouvé !.");
    }
}
