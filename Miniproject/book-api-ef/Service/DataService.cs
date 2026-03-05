using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private BookContext db { get; }

    public DataService(BookContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {

        Post posts = db.Posts.FirstOrDefault()!;
        if (posts == null)
        {
            posts = new Post
            {
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
                        CommenterName = "Basviola",
                        Tekst = "Jeg elsker dig Jesper!!! Glæder mig til rave",
                        Date = DateTime.Now.AddDays(+6),
                        Upvotes = 100,
                        Downvotes = 10,
                    }
                }
            };
            db.Posts.Add(posts);

            db.Posts.Add(new Post
            {
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
                        CommenterName = "Farquaad",
                        Tekst = "Grise er mega klamme, fy for satan",
                        Date = DateTime.Now.AddDays(-2),
                        Upvotes = 50,
                        Downvotes = 100,
                    }
                }
            });

            db.Posts.Add(new Post
            {
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
                        CommenterName = "Shrek",
                        Tekst = "Hold kæft der skal kværnes bajere lille fredag",
                        Date = DateTime.Now.AddDays(+11),
                        Upvotes = 999,
                        Downvotes = 0,
                    }
                }
            });
        }

        db.SaveChanges();
    }

    public List<Post> GetPosts()
    {
        return db.Posts
            .OrderByDescending(p => p.Date)
            .ToList();
    }

    public Post? GetPost(int id)
    {
        return db.Posts.Include(a => a.Comments).FirstOrDefault(a => a.PostId == id);
    }

    public string CreatePost(string author, string postname, string content)
    {
        db.Posts.Add(new Post
        {
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

    public string CreateComment(int postId, string commenterName, string tekst)
    {
        Post post = db.Posts.FirstOrDefault(a => a.PostId == postId);
        if (post != null)
        {
            post.Comments.Add(new Comment
            {
                CommenterName = commenterName,
                Tekst = tekst,
                Date = DateTime.Now,
                Upvotes = 0,
                Downvotes = 0,
            });
            db.SaveChanges();
            return "Comment created";
        }

        return "Post not found";
    }

    public async Task UpvotePost(int postId)
    {
        Post post = db.Posts.FirstOrDefault(a => a.PostId == postId);
        if (post != null)
        {
            post.upvotes++;
        }
        db.SaveChanges();
    }
    public async Task DownvotePost(int postId)
    {
        Post post = db.Posts.FirstOrDefault(a => a.PostId == postId);
        if (post != null)
        {
            post.downvotes++;
        }
        db.SaveChanges();
    }
    public async Task UpvoteComment(int commentId)
    {
        Comment comment = db.Comments.FirstOrDefault(a => a.CommentId == commentId);
        if (comment != null)
        {
            comment.Upvotes++;
        }
        db.SaveChanges();
    }
    public async Task DownvoteComment(int commentId)
    {
        Comment comment = db.Comments.FirstOrDefault(a => a.CommentId == commentId);
        if (comment != null)
        {
            comment.Upvotes++;
        }
        db.SaveChanges();
    }
}