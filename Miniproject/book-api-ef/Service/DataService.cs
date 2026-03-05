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
                Date = DateTime.Now.AddDays(+5),
                upvotes = 99,
                downvotes = 2,
                Comments = new List<Comment>
                {
                    new Comment()
                    {
                        CommentId = 1,
                        CommenterName = "Basviola",
                        Tekst = "Jeg elsker dig Jesper!!! Glæder mig til rave",
                        Date = DateTime.Now.AddDays(+6),
                        Upvotes = 100,
                        Downvotes = 10,
                    }
                }
            };
            db.Posts.Add(posts);
            
            db.Posts.Add(new Posts { 
                PostId = 2,
                Author = "Oskar",
                Postname = "Kælegrise",
                Content = "Jeg synes grise er mega nice",
                Date = DateTime.Now.AddDays(-3),
                upvotes = 999,
                downvotes = 10,
                Comments = new List<Comment>
                {
                    new Comment()
                    {
                        CommentId = 1,
                        CommenterName = "Farquaad",
                        Tekst = "Grise er mega klamme, fy for satan",
                        Date = DateTime.Now.AddDays(-2),
                        Upvotes = 50,
                        Downvotes = 100,
                    }
                }
            });
            
            db.Posts.Add(new Posts { 
                PostId = 3,
                Author = "Simon",
                Postname = "Basement er nice",
                Content = "Der er tilbud på vodka redbull og IPA på torsdag",
                Date = DateTime.Now.AddDays(+10),
                upvotes = 100,
                downvotes = 5,
                Comments = new List<Comment>
                {
                new Comment()
                {
                CommentId = 1,
                CommenterName = "Shrek",
                Tekst = "Hold kæft der skal kværnes bajere lille fredag",
                Date = DateTime.Now.AddDays(+11),
                Upvotes = 999,
                Downvotes = 0,
            }
            }});
        }
        db.SaveChanges();
    }

    public List<Posts> GetPosts() {
        return db.Posts
            .OrderByDescending(p => p.Date)
            .ToList();
    }

    public Posts GetPost(int id) {
        return db.Posts.Include(a => a.Comments).FirstOrDefault(a => a.PostId == id);
    }

    public string CreatePost(string author, string postname, string content) {
        db.Posts.Add(new Posts { 
            Author = author,  
            Postname = postname, 
            Content = content, 
            Date = DateTime.Now, 
            upvotes = 0, 
            downvotes = 0
            
        });
        db.SaveChanges();
        return "Post created";
    }
    public string CreateComment(int postId, string commenterName, string tekst) {
        Posts posts = db.Posts.FirstOrDefault(a => a.PostId == postId);
        db.Comments.Add(new Comment { 
            CommenterName = commenterName, 
            Tekst = tekst,
            Date = DateTime.Now,
            Upvotes = 0,
            Downvotes = 0,
        });
        db.SaveChanges();
        return "Comment created";
    }

}