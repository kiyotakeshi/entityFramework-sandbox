using System.Collections.Generic;

namespace sandbox.models
{
  public class Author
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; }
  }
}
