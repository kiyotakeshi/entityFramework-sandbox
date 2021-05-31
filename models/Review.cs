namespace sandbox.models
{
    public class Review
    {
        public int Id { get; set; }

        public string Headline { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }

        public Reviewer Reviewer { get; set; }
        public Book Book { get; set; }
    }
}