using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Quizz_API.Models;

namespace Project_Quizz_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Quiz_Question> QuizQuestions { get; set; }
        public virtual DbSet<Quiz_Answer> QuizAnswers { get; set; }

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

            builder.Entity<Quiz_Answer>()
                .HasOne(a => a.Quiz_Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
        }

    }
}
