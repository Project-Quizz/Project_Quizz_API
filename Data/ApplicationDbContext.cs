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
		public virtual DbSet<Quiz_Categorie> Quiz_Categories { get; set; }
		public virtual DbSet<Multi_Quiz> Multi_Quizzes { get; set; }
		public virtual DbSet<Multi_Quiz_Player> Multi_Quiz_Players { get; set; }
		public virtual DbSet<Multi_Quiz_Attempt> Multi_Quiz_Attempts { get; set; }
		public virtual DbSet<Multi_Given_Answer_Attempt> Multi_Given_Answer_Attempts { get; set; }
		public virtual DbSet<Single_Given_Answer_Attepmt> Single_Given_Answer_Attepmts { get; set; }

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
				.HasMany(a => a.SingleQuizQuestionAnswers)
				.WithOne(a => a.Single_Quiz_Attempt)
				.HasForeignKey(a => a.SingleQuizAttemptId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Single_Given_Answer_Attepmt>()
				.HasOne(sqaa => sqaa.Single_Quiz_Attempt)
				.WithMany(sqa => sqa.SingleQuizQuestionAnswers)
				.HasForeignKey(sqaa => sqaa.SingleQuizAttemptId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Single_Given_Answer_Attepmt>()
				.HasOne(sqaa => sqaa.Quiz_Question_Answer)
				.WithMany(qqa => qqa.SingleQuizAttemptAnswers)
				.HasForeignKey(sqaa => sqaa.QuizQuestionAnswerId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Quiz_Question_Answer>()
				.HasMany(qqa => qqa.SingleQuizAttemptAnswers)
				.WithOne(sqaa => sqaa.Quiz_Question_Answer)
				.HasForeignKey(sqaa => sqaa.QuizQuestionAnswerId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Quiz_Question>()
				.HasOne(a => a.Quiz_Categorie)
				.WithMany(a => a.Questions)
				.HasForeignKey(a => a.QuizCategorieId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Single_Quiz>()
				.HasOne(a => a.Quiz_Categorie)
				.WithMany(a => a.SingleQuizzes)
				.HasForeignKey(a => a.QuizCategorieId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Multi_Quiz>()
				.HasMany(a => a.Multi_Quiz_Attempts)
				.WithOne(a => a.Multi_Quiz)
				.HasForeignKey(a => a.MultiQuizId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Multi_Quiz>()
				.HasOne(a => a.Quiz_Categorie)
				.WithMany()
				.HasForeignKey(a => a.QuizCategorieId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Multi_Quiz>()
				.HasMany(a => a.Multi_Quiz_Players)
				.WithOne(a => a.Multi_Quiz)
				.HasForeignKey(a => a.MultiQuizId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Multi_Quiz_Player>()
				.HasMany(a => a.Multi_Quiz_Attempts)
				.WithOne(a => a.Multi_Quiz_Player)
				.HasForeignKey(a => a.MultiQuizPlayerId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Multi_Quiz_Attempt>()
				.HasOne(a => a.Quiz_Question)
				.WithMany()
				.HasForeignKey(a => a.AskedQuestionId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Multi_Quiz_Attempt>()
				.HasMany(a => a.MultiQuizQuestionAnswers)
				.WithOne(a => a.Multi_Quiz_Attempt)
				.HasForeignKey(a => a.MultiQuizAttemptId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Multi_Given_Answer_Attempt>()
				.HasOne(sqaa => sqaa.Multi_Quiz_Attempt)
				.WithMany(sqa => sqa.MultiQuizQuestionAnswers)
				.HasForeignKey(sqaa => sqaa.MultiQuizAttemptId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Multi_Given_Answer_Attempt>()
				.HasOne(sqaa => sqaa.Quiz_Question_Answer)
				.WithMany(qqa => qqa.MultiQuizAttemptAnswers)
				.HasForeignKey(sqaa => sqaa.QuizQuestionAnswerId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Quiz_Question_Answer>()
				.HasMany(qqa => qqa.MultiQuizAttemptAnswers)
				.WithOne(mqaa => mqaa.Quiz_Question_Answer)
				.HasForeignKey(mqaa => mqaa.QuizQuestionAnswerId)
				.OnDelete(DeleteBehavior.Cascade);
		}

	}
}
