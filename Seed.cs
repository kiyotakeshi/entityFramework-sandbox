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
          if(context.Books.Any()) return;
          var book = new Book()
                    {
                        Title = "Big Romantic Book",
                        Published = new DateTime(1879,3,2),
                        Reviews = new List<Review>()
                        {
                            new Review
                            {
                              Headline = "Romantic Book",
                              ReviewText = "This book made me cry",
                              Rating = 5,
                              Reviewer = new Reviewer()
                                {
                                  FirstName = "Yamada",
                                  LastName = "Taro"
                                }
                            },
                            new Review
                            {
                              Headline = "Horrible adventure",
                              ReviewText = "Waste my time",
                              Rating = 1,
                              Reviewer = new Reviewer()
                                {
                                  FirstName = "Sato",
                                  LastName = "Jiro"
                                }
                            },
                        }
                      };
          await context.Books.AddAsync(book);
          // await context.Books.AddRangeAsync(book);
          await context.SaveChangesAsync();
        }
    }
}
