using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.DatabaseModels
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

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<BlockedTag> BlockedTags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<DiscussionTopic> DiscussionTopics { get; set; }
        public virtual DbSet<FollowingMovie> FollowingMovies { get; set; }
        public virtual DbSet<FollowingUser> FollowingUsers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieDirector> MovieDirectors { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<MovieLanguage> MovieLanguages { get; set; }
        public virtual DbSet<MovieTag> MovieTags { get; set; }
        public virtual DbSet<NewComment> NewComments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configured in WebAPI/Startup.cs for DI
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorName)
                    .HasName("PK__actors__C4B3E035C299C477");

                entity.ToTable("actors");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("actor_name");
            });

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

            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasKey(e => e.DirectorName)
                    .HasName("PK__director__CD35ECA3F4D080AE");

                entity.ToTable("directors");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("director_name");
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

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreName)
                    .HasName("PK__genres__1E98D1506ED22919");

                entity.ToTable("genres");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageName)
                    .HasName("PK__language__9CE82382763B02B8");

                entity.ToTable("languages");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("language_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.Awards)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("awards");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Plot)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("plot");

                entity.Property(e => e.PosterLink)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("poster_link");

                entity.Property(e => e.Rated)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rated");

                entity.Property(e => e.Released)
                    .HasColumnType("date")
                    .HasColumnName("released");

                entity.Property(e => e.RuntimeMinutes)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("runtime_minutes");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Year)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("year");
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.ActorName })
                    .HasName("movieID_actor_pk");

                entity.ToTable("movie_actors");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("actor_name");

                entity.HasOne(d => d.ActorNameNavigation)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.ActorName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_act__actor__06CD04F7");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_act__movie__05D8E0BE");
            });

            modelBuilder.Entity<MovieDirector>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.DirectorName })
                    .HasName("movieID_director_pk");

                entity.ToTable("movie_directors");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("director_name");

                entity.HasOne(d => d.DirectorNameNavigation)
                    .WithMany(p => p.MovieDirectors)
                    .HasForeignKey(d => d.DirectorName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_dir__direc__01142BA1");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieDirectors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_dir__movie__00200768");
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreName })
                    .HasName("movieID_genre_pk");

                entity.ToTable("movie_genres");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("genre_name");

                entity.HasOne(d => d.GenreNameNavigation)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.GenreName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_gen__genre__7B5B524B");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_gen__movie__7A672E12");
            });

            modelBuilder.Entity<MovieLanguage>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.LanguageName })
                    .HasName("movieID_language_pk");

                entity.ToTable("movie_languages");

                entity.Property(e => e.MovieId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("movieID");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("language_name");

                entity.HasOne(d => d.LanguageNameNavigation)
                    .WithMany(p => p.MovieLanguages)
                    .HasForeignKey(d => d.LanguageName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_lan__langu__0C85DE4D");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieLanguages)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__movie_lan__movie__0B91BA14");
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
