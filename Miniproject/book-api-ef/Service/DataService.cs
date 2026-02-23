using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private BookContext db { get; }

    public DataService(BookContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        
        Posts posts = db.Posts.FirstOrDefault()!;
        if (posts == null) {
            posts = new Posts { Postname = "Kristian" };
            db.Posts.Add(posts);
            db.Posts.Add(new Posts { Postname = "Søren" });
            db.Posts.Add(new Posts { Postname = "Mette" });
        }

        Comment book = db.Comments.FirstOrDefault()!;
        if (book == null)
        {
            db.Comments.Add(new Comment { Title = "Harry Potter", Author = posts });
            db.Comments.Add(new Comment { Title = "Ringenes Herre", Author = posts });
            db.Comments.Add(new Comment { Title = "Entity Framework for Dummies", Author = posts });
        }

        db.SaveChanges();
    }

    public List<Comment> GetComments() {
        return db.Comments.Include(b => b.CommentId).ToList();
    }

    public Comment GetComments(int id) {
        return db.Comments.Include(b => b.CommentId).FirstOrDefault(b => b.CommentId == id);
    }

    public List<Posts> GetPosts() {
        return db.Posts.ToList();
    }

    public Posts GetPosts(int id) {
        return db.Posts.Include(a => a.Comments).FirstOrDefault(a => a.PostId == id);
    }

    public string CreateComment(string commenterName, int postId) {
        Posts posts = db.Posts.FirstOrDefault(a => a.PostId == postId);
        db.Comments.Add(new Comment { CommenterName = commenterName, Posts = posts, CommentBody = });
        db.SaveChanges();
        return "Comment created";
    }

}