﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Quizz_API.Data;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240403151600_implemenMultiQuizzesTables")]
    partial class implemenMultiQuizzesTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuizCategorieId")
                        .HasColumnType("int");

                    b.Property<bool>("QuizCompleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("QuizCategorieId");

                    b.ToTable("Multi_Quizzes");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz_Attempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AskedQuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("GivenAnswerId")
                        .HasColumnType("int");

                    b.Property<int>("MultiQuizId")
                        .HasColumnType("int");

                    b.Property<int>("MultiQuizPlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AskedQuestionId");

                    b.HasIndex("MultiQuizId");

                    b.HasIndex("MultiQuizPlayerId");

                    b.ToTable("Multi_Quiz_Attempts");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz_Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MultiQuizId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MultiQuizId");

                    b.ToTable("Multi_Quiz_Players");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Quiz_Categories");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuizCategorieId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuizCategorieId");

                    b.ToTable("Quiz_Questions");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Question_Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrectAnswer")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Quiz_Question_Answers");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Single_Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuizCategorieId")
                        .HasColumnType("int");

                    b.Property<bool>("QuizCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuizCategorieId");

                    b.ToTable("Single_Quizzes");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Single_Quiz_Attempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AskedQuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("GivenAnswerId")
                        .HasColumnType("int");

                    b.Property<int>("SingleQuizId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AskedQuestionId");

                    b.HasIndex("GivenAnswerId");

                    b.HasIndex("SingleQuizId");

                    b.ToTable("Single_Quiz_Attempts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Categorie", "Quiz_Categorie")
                        .WithMany()
                        .HasForeignKey("QuizCategorieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Quiz_Categorie");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz_Attempt", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Question", "Quiz_Question")
                        .WithMany()
                        .HasForeignKey("AskedQuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Project_Quizz_API.Models.Quiz_Question_Answer", "Quiz_Question_Answer")
                        .WithMany()
                        .HasForeignKey("AskedQuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Project_Quizz_API.Models.Multi_Quiz", "Multi_Quiz")
                        .WithMany("Multi_Quiz_Attempts")
                        .HasForeignKey("MultiQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Quizz_API.Models.Multi_Quiz_Player", "Multi_Quiz_Player")
                        .WithMany("Multi_Quiz_Attempts")
                        .HasForeignKey("MultiQuizPlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Multi_Quiz");

                    b.Navigation("Multi_Quiz_Player");

                    b.Navigation("Quiz_Question");

                    b.Navigation("Quiz_Question_Answer");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz_Player", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Multi_Quiz", "Multi_Quiz")
                        .WithMany("Multi_Quiz_Players")
                        .HasForeignKey("MultiQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Multi_Quiz");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Question", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Categorie", "Quiz_Categorie")
                        .WithMany("Questions")
                        .HasForeignKey("QuizCategorieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Quiz_Categorie");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Question_Answer", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Question", "Quiz_Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz_Question");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Single_Quiz", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Categorie", "Quiz_Categorie")
                        .WithMany("SingleQuizzes")
                        .HasForeignKey("QuizCategorieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Quiz_Categorie");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Single_Quiz_Attempt", b =>
                {
                    b.HasOne("Project_Quizz_API.Models.Quiz_Question", "Quiz_Question")
                        .WithMany()
                        .HasForeignKey("AskedQuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Project_Quizz_API.Models.Quiz_Question_Answer", "Quiz_Question_Answer")
                        .WithMany()
                        .HasForeignKey("GivenAnswerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Project_Quizz_API.Models.Single_Quiz", "Single_Quiz")
                        .WithMany("Quiz_Attempts")
                        .HasForeignKey("SingleQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz_Question");

                    b.Navigation("Quiz_Question_Answer");

                    b.Navigation("Single_Quiz");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz", b =>
                {
                    b.Navigation("Multi_Quiz_Attempts");

                    b.Navigation("Multi_Quiz_Players");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Multi_Quiz_Player", b =>
                {
                    b.Navigation("Multi_Quiz_Attempts");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Categorie", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("SingleQuizzes");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Quiz_Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Project_Quizz_API.Models.Single_Quiz", b =>
                {
                    b.Navigation("Quiz_Attempts");
                });
#pragma warning restore 612, 618
        }
    }
}
