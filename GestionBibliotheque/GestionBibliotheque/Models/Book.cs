using System;

namespace GestionBibliotheque.Models
{
	public class Book
	{
		public static int Id { get; set; } = 0;
		public int IdBook { get; set; }
		public string Title { get; set; }
		public string ISBN { get; set; }
		public decimal Price { get; set; }
		public string Author { get; set; }
		public int Year { get; set; }
		public int Copies { get; set; }

		public Book()
		{
			IdBook = ++Id;
		}

   
    }
}

