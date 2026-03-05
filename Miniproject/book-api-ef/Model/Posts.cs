namespace Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Author {get; set;}
        public string? Postname { get; set; }
        public DateTime Date {get; set;}
        public string Content {get; set;}
        public int upvotes { get; set; }
        public int downvotes { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}