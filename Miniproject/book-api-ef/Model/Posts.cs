namespace Model
{
    public class Posts
    {
        public int PostId { get; set; }
        
        public string Author {get; set;}
        public string Postname { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        
        public DateTime Date {get; set;}
        
        public int upvotes { get; set; }
        public int downvotes { get; set; }
    }
}