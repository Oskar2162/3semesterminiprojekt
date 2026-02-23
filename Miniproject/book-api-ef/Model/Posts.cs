namespace Model
{
    public class Posts
    {
        public int PostId { get; set; }
        public string Postname { get; set; }
        public List<Comment> Comments { get; set; }
    }
}