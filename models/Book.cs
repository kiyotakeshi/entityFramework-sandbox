using System;
using System.Collections.Generic;

namespace sandbox.models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Published { get; set; }

        public ICollection<Review> Reviews  { get; set; }

        public ICollection<BookAuthor> BookAuthors  { get; set; }

    }
}