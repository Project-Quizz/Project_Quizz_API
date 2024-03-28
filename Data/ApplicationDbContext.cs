using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Quizz_API.Models;

namespace Project_Quizz_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Quiz_Question> Quiz_Questions { get; set; }
        public virtual DbSet<Quiz_Question_Answer> Quiz_Question_Answers { get; set; }
        public virtual DbSet<Single_Quiz> Single_Quizzes { get; set; }
        public virtual DbSet<Single_Quiz_Attempt> Single_Quiz_Attempts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Quiz_Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Quiz_Question)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Quiz_Question_Answer>()
                .HasOne(a => a.Quiz_Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<Single_Quiz>()
                .HasMany(a => a.Quiz_Attempts)
                .WithOne(a => a.Single_Quiz)
                .HasForeignKey(a => a.SingleQuizId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Single_Quiz_Attempt>()
                .HasOne(a => a.Quiz_Question)
                .WithMany()
                .HasForeignKey(a => a.AskedQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Single_Quiz_Attempt>()
                .HasOne(a => a.Quiz_Question)
                .WithMany()
                .HasForeignKey(a => a.GivenAnswerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
