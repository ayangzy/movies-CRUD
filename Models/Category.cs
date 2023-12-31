﻿using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
	public class Category
	{
		[Key]

		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

		[DisplayNameAttribute("Display Order")]
		[Range(1,100, ErrorMessage = "Display order must be between 1 and 100 only")]
		public int DisplayOrder { get; set; }

		public DateTime CreatedDateTime { get; set; } = DateTime.Now;


		public Category()
		{
		}
	}
}

