using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCore.Models;
using System;

namespace ProjectData
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<News> Newss { get; set; }
    }
}
