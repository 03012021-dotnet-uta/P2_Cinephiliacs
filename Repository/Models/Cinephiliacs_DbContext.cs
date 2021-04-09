using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.Models
{
    public partial class Cinephiliacs_DbContext : DbContext
    {
        public Cinephiliacs_DbContext()
        {
        }

        public Cinephiliacs_DbContext(DbContextOptions<Cinephiliacs_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockedTag> BlockedTags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<DiscussionTopic> DiscussionTopics { get; set; }
        public virtual DbSet<FollowingMovie> FollowingMovies { get; set; }
        public virtual DbSet<FollowingUser> FollowingUsers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieTag> MovieTags { get; set; }
        public virtual DbSet<NewComment> NewComments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BlockedTag>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.TagName })
                    .HasName("username_tag_pk");

                entity.ToTable("blocked_tags");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.TagName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tag_name");

                entity.HasOne(d => d.TagNameNavigation)
                    .WithMany(p => p.BlockedTags)
                    .HasForeignKey(d => d.TagName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__blocked_t__tag_n__628FA481");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.BlockedTags)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__blocked_t__usern__619B8048");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.CommentId).HasColumnName("commentID");

                entity.Property(e => e.CommentText)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("comment_text");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");

                entity.Property(e => e.DiscussionId).HasColumnName("discussionID");

                entity.Property(e => e.IsSpoiler).HasColumnName("is_spoiler");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.DiscussionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comments__discus__18EBB532");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comments__userna__19DFD96B");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.ToTable("discussions");

                entity.Property(e => e.DiscussionId).HasColumnName("discussionID");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");

                entity.Property(e => e.MovieId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__discussio__movie__114A936A");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__discussio__usern__123EB7A3");
            });

            modelBuilder.Entity<DiscussionTopic>(entity =>
            {
                entity.HasKey(e => new { e.DiscussionId, e.TopicName })
                    .HasName("discussionID_topic_pk");

                entity.ToTable("discussion_topics");

                entity.Property(e => e.DiscussionId).HasColumnName("discussionID");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("topic_name");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.DiscussionTopics)
                    .HasForeignKey(d => d.DiscussionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__discussio__discu__151B244E");

                entity.HasOne(d => d.TopicNameNavigation)
                    .WithMany(p => p.DiscussionTopics)
                    .HasForeignKey(d => d.TopicName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__discussio__topic__160F4887");
            });

            modelBuilder.Entity<FollowingMovie>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.MovieId })
                    .HasName("user_following_movie_pk");

                entity.ToTable("following_movies");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.FollowingMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__movie__71D1E811");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.FollowingMovies)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__usern__70DDC3D8");
            });

            modelBuilder.Entity<FollowingUser>(entity =>
            {
                entity.HasKey(e => new { e.Follower, e.Followee })
                    .HasName("follower_followee_pk");

                entity.ToTable("following_users");

                entity.Property(e => e.Follower)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("follower");

                entity.Property(e => e.Followee)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("followee");

                entity.HasOne(d => d.FolloweeNavigation)
                    .WithMany(p => p.FollowingUserFolloweeNavigations)
                    .HasForeignKey(d => d.Followee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__follo__66603565");

                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.FollowingUserFollowerNavigations)
                    .HasForeignKey(d => d.Follower)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__follo__656C112C");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");
            });

            modelBuilder.Entity<MovieTag>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.TagName, e.Username })
                    .HasName("movieID_tag_pk");

                entity.ToTable("movie_tags");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.TagName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tag_name");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.IsUpvote)
                    .IsRequired()
                    .HasColumnName("is_upvote")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieTags)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_tag__movie__6C190EBB");

                entity.HasOne(d => d.TagNameNavigation)
                    .WithMany(p => p.MovieTags)
                    .HasForeignKey(d => d.TagName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_tag__tag_n__6D0D32F4");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.MovieTags)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_tag__usern__6E01572D");
            });

            modelBuilder.Entity<NewComment>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.CommentId })
                    .HasName("user_comment_pk");

                entity.ToTable("new_comments");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.CommentId).HasColumnName("commentID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.NewComments)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__new_comme__comme__1DB06A4F");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.NewComments)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__new_comme__usern__1CBC4616");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.MovieId })
                    .HasName("user_movie_review_pk");

                entity.ToTable("reviews");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");

                entity.Property(e => e.Rating)
                    .HasColumnType("numeric(2, 1)")
                    .HasColumnName("rating");

                entity.Property(e => e.Review1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("review");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reviews__movieID__75A278F5");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reviews__usernam__74AE54BC");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagName)
                    .HasName("PK__tags__E298655D004B142D");

                entity.ToTable("tags");

                entity.Property(e => e.TagName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tag_name");

                entity.Property(e => e.IsBanned).HasColumnName("is_banned");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.TopicName)
                    .HasName("PK__topics__54BAE5EDDE491A72");

                entity.ToTable("topics");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("topic_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__users__F3DBC573D3C2F90C");

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Permissions).HasColumnName("permissions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
