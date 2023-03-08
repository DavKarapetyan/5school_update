using _5school.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL
{
    public class _5schoolDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public _5schoolDbContext(DbContextOptions<_5schoolDbContext> options) : base(options) { }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<_5school.DAL.Entities.Stream> Streams { get; set; }
        public DbSet<SubStream> SubStreams { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
