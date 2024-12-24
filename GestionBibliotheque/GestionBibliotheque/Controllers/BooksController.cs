using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionBibliotheque.DTOs;
using GestionBibliotheque.Exceptions;
using GestionBibliotheque.Models;
using GestionBibliotheque.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GestionBibliotheque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetBooks()
        {
            IList<BookDto> books = _bookService.GetAllBooks();
            return Ok(books);
        }
        // GET api/values/5
        [HttpGet("filtred")]
        public IActionResult GetBookByIdAndTitle([FromQuery] BookFilterDto filterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }       
                BookDto book = _bookService.GetBookByIdAndTitle(filterDto);
                return Ok(book);
        }
        

        // POST api/values
        [HttpPost]
        public IActionResult AddBook([FromBody] BookAddDto bookVM)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
                Book book = _bookService.AddBook(bookVM);
                return CreatedAtAction(nameof(GetBookByIdAndTitle), new { id = book.IdBook , title = book.Title} , book);
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody]BookUpdateDto bookVm)
        {
            if(id != bookVm.Id)
            {
                return BadRequest();
            }
            _bookService.UpdateBook(id, bookVm);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}

