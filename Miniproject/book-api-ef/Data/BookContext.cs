using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class BookContext : DbContext
    {
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Post> Posts => Set<Post>();


        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}