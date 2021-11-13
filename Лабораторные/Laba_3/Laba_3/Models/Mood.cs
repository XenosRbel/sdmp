using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_3.Models
{
	internal class Mood : IMood
	{
		public string Title { get; set; }
		public string ImageUrl { get; set; }
		public MoodType MoodType { get; set; }
	}
}
