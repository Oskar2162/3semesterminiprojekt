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
        if (posts == null)
        {
            posts = new Posts
            {
                PostId = 1,
                Author = "Jesper",
                Postname = "Psytrance rave d. 01/04",
                Content = "Kom til vildt raveparty med godt humør, masser af sjov og psytrance",
                Date = DateTime.Now,
                upvotes = 99,
                downvotes = 2,
            };
            db.Posts.Add(posts);
            
            db.Posts.Add(new Posts { 
                PostId = 2,
                Author = "Oskar",
                Postname = "Kælegrise",
                Content = "Jeg synes grise er mega nice",
                Date = DateTime.Now,
                upvotes = 999,
                downvotes = 10, });
            
            db.Posts.Add(new Posts { 
                PostId = 3,
                Author = "Simon",
                Postname = "Basement er nice",
                Content = "Der er tilbud på vodka redbull og IPA på torsdag",
                Date = DateTime.Now,
                upvotes = 100,
                downvotes = 5, });
        }
        db.SaveChanges();
    }

    public List<Posts> GetPosts() {
        return db.Posts
            .OrderByDescending(p => p.Date)
            .ToList();
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