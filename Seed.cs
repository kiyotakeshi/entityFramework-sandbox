using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sandbox.models;

namespace entityFramework_sandbox
{
  public class Seed
  {
    public static async Task SeedData(DataContext context)
    {
      var author1 = new Author()
      {
        FirstName = "Jack",
        LastName = "London",
      };
      var author2 = new Author()
      {
        FirstName = "Pavol",
        LastName = "Almasi",
      };
      var book1 = new Book()
      {
        Title = "Big Romantic Book",
        Published = new DateTime(1879, 3, 2),
        Reviews = new List<Review>()
                        {
                            new Review { Headline = "Good Romantic Book", ReviewText = "This book made me cry a few times", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Allison", LastName = "Kutz" } },
                            new Review { Headline = "Horrible Romantic Book", ReviewText = "My wife made me read it and I hated it", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Kyle", LastName = "Kutz" } }
                        }
      };
      var booksAuthors = new List<BookAuthor>()
      {
        new BookAuthor()
        {
            Book = new Book()
            {
                Title = "The Call Of The Wild",
                Published = new DateTime(1903,1,1),
                Reviews = new List<Review>()
                {
                    new Review { Headline = "Awesome Book", ReviewText = "Reviewing Call of the Wild and it is awesome beyond words", Rating = 5,
                        Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                    new Review { Headline = "Terrible Book", ReviewText = "Reviewing Call of the Wild and it is terrrible book", Rating = 1,
                        Reviewer = new Reviewer(){ FirstName = "Peter", LastName = "Griffin" } },
                    new Review { Headline = "Decent Book", ReviewText = "Not a bad read, I kind of liked it", Rating = 3,
                        Reviewer = new Reviewer(){ FirstName = "Paul", LastName = "Griffin" } }
                }
            },
            Author = author1
        },
        new BookAuthor()
        {
            Book = new Book()
            {
                Title = "Winnetou",
                Published = new DateTime(1878,10,1),
                Reviews = new List<Review>()
                {
                    new Review { Headline = "Awesome Western Book", ReviewText = "Reviewing Winnetou and it is awesome book", Rating = 4,
                        Reviewer = new Reviewer(){ FirstName = "Frank", LastName = "Gnocci" } }
                }
            },
            Author = author1
        },
        new BookAuthor()
        {
            Book = new Book()
            {
                Title = "Pavols Best Book",
                Published = new DateTime(2019,2,2),
                Reviews = new List<Review>()
                {
                    new Review { Headline = "Awesome Programming Book", ReviewText = "Reviewing Pavols Best Book and it is awesome beyond words", Rating = 5,
                        Reviewer = new Reviewer(){ FirstName = "Pavol2", LastName = "Almasi2" } }
                }
            },
            Author = author2
        },
        new BookAuthor()
        {
            Book = new Book()
            {
                Title = "Three Musketeers",
                Published = new DateTime(2019,2,2),
            },
            Author = author2
        },
        new BookAuthor()
        {
            Book = book1,
            Author = new Author()
            {
              FirstName = "Anita",
              LastName = "Powers",
            }
        },
        new BookAuthor()
        {
          Book = book1,
            Author = new Author()
            {
              FirstName = "Powers",
              LastName = "Anita",
            }
        }
      };
      await context.BookAuthors.AddRangeAsync(booksAuthors);
      await context.SaveChangesAsync();
    }
  }
}
