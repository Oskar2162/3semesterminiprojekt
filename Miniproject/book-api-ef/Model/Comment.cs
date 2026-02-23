namespace Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommenterName { get; set; }
        
        public string Tekst { get; set; }
        
        public int Upvotes { get; set; }
        
        public int Downvotes { get; set; }
        
        public DateTime date {get; set;}
    }
}