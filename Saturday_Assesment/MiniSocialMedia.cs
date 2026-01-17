using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.IO;
using System.Text;

namespace MiniSocialMedia
{
    public class SocialException : Exception
    {
        public SocialException(string message) : base(message) { }
        public SocialException(string message, Exception inner) : base(message, inner) { }
    }
    public interface IPostable
    {
        void AddPost(string content);
        IReadOnlyList<Post> GetPosts();
    }
    public class Post
    {
        public User Author { get; }
        public string Content { get; }
        public DateTime CreatedAt { get; }

        public Post(User author, string content)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Author} • {CreatedAt.FormatTimeAgo()}");
            sb.AppendLine(Content);

            var hashtags = Regex.Matches(Content, @"#\w+");
            if (hashtags.Count > 0)
            {
                sb.Append("Tags: ");
                sb.AppendJoin(", ", hashtags.Cast<Match>().Select(m => m.Value));
            }

            return sb.ToString().TrimEnd();
        }
    }
    public partial class User : IPostable, IComparable<User>
    {
        public string Username { get; init; }
        public string Email { get; init; }

        private readonly List<Post> _posts = new();
        private readonly HashSet<string> _following = new(StringComparer.OrdinalIgnoreCase);

        public event Action<Post>? OnNewPost;

        public User(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email?.Trim() ?? "", emailPattern))
                throw new SocialException("Invalid email format");

            Username = username.Trim();
            Email = email.Trim().ToLower();
        }

        public void Follow(string username)
        {
            if (string.Equals(Username, username, StringComparison.OrdinalIgnoreCase))
                throw new SocialException("Cannot follow yourself");

            _following.Add(username.Trim());
        }

        public bool IsFollowing(string username) => _following.Contains(username);

        public void AddPost(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Post cannot be empty");

            if (content.Length > 280)
                throw new SocialException("Post too long (max 280 characters)");

            var post = new Post(this, content.Trim());
            _posts.Add(post);

            OnNewPost?.Invoke(post);
        }

        public IReadOnlyList<Post> GetPosts() => _posts.AsReadOnly();

        public int CompareTo(User? other)
        {
            if (other == null) return 1;
            return string.Compare(Username, other.Username, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString() => $"@{Username}";
    }

    public partial class User
    {
        public string GetDisplayName() => $"User: {Username} <{Email}>";
    }
    public class Repository<T> where T : class
    {
        private readonly List<T> _items = new();

        public void Add(T item) => _items.Add(item);

        public IReadOnlyList<T> GetAll() => _items.AsReadOnly();

        public T? Find(Predicate<T> match) => _items.Find(match);
    }
    public static class SocialUtils
    {
        public static string FormatTimeAgo(this DateTime dateTime)
        {
            var diff = DateTime.UtcNow - dateTime;
            if (diff.TotalMinutes < 1) return "just now";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} min ago";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} h ago";
            return dateTime.ToString("MMM dd");
        }
    }

    public static class UserExtensions
    {
        public static IEnumerable<string> GetFollowingNames(this User user)
        {
            return new List<string>();
        }
    }

    class Program
    {
        private static readonly Repository<User> _users = new();
        private static User? _currentUser;
        private static readonly string _dataFile = "social-data.json";

        static void Main(string[] args)  // main ------------------
        {
            Console.Title = "MiniSocial - Console Edition";
            Console.WriteLine("=== MiniSocial ===");
            LoadData();

            while (true)
            {
                try
                {
                    if (_currentUser == null)
                        ShowLoginMenu();
                    else
                        ShowMainMenu();
                }
                catch (SocialException ex)
                {
                    ConsoleColorWrite(ConsoleColor.Yellow, $"Error: {ex.Message}");
                    if (ex.InnerException != null) Console.WriteLine($" -> {ex.InnerException.Message}");
                }
                catch (Exception ex)
                {
                    ConsoleColorWrite(ConsoleColor.Red, $"Critical Error: {ex.Message}");
                    LogError(ex);
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowLoginMenu()
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Enter Option : ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": Register(); break;
                case "2": Login(); break;
                case "3": SaveData(); Environment.Exit(0); break;
                default: ConsoleColorWrite(ConsoleColor.Red, "Invalid option."); break;
            }
        }

        static void Register()
        {
            Console.Write("Username: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            if (_users.Find(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase)) != null)
                throw new SocialException("Username already exists");

            var newUser = new User(name, email);
            _users.Add(newUser);
            ConsoleColorWrite(ConsoleColor.Green, $"Welcome {newUser.Username}");
        }

        static void Login()
        {
            Console.Write("Username: ");
            string name = Console.ReadLine() ?? "";
            var user = _users.Find(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (user == null) throw new SocialException("User not found.");

            _currentUser = user;
            ConsoleColorWrite(ConsoleColor.Green, $"Successfully logged in as {user.Username}");

            foreach (var u in _users.GetAll())
            {
                if (_currentUser.IsFollowing(u.Username))
                {
                    u.OnNewPost += (post) => 
                        ConsoleColorWrite(ConsoleColor.Cyan, $"\n[NOTIF] {post.Author} just posted!");
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine($"--- Logged in as {_currentUser?.Username} ---");
            Console.WriteLine("1. Post Message\n2. View Own Posts\n3. View Timeline\n4. Follow User\n5. List Users\n6. Logout\n7. Exit");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": PostMessage(); break;
                case "2": ShowPosts(_currentUser?.GetPosts()); break;
                case "3": ShowTimeline(); break;
                case "4": FollowUser(); break;
                case "5": ListUsers(); break;
                case "6": _currentUser = null; break;
                case "7": SaveData(); Environment.Exit(0); break;
            }
        }

        static void PostMessage()
        {
            Console.Write("Write post (max 280 chars): ");
            string content = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(content)) return;

            _currentUser?.AddPost(content);
            ConsoleColorWrite(ConsoleColor.Green, "Post published!");
        }

        static void ShowTimeline()
        {
            var timeline = new List<Post>();
            timeline.AddRange(_currentUser!.GetPosts());

            var followedPosts = _users.GetAll()
                .Where(u => _currentUser.IsFollowing(u.Username))
                .SelectMany(u => u.GetPosts());

            timeline.AddRange(followedPosts);
            ShowPosts(timeline.OrderByDescending(p => p.CreatedAt).ToList());
        }

        static void ShowPosts(IEnumerable<Post>? posts)
        {
            if (posts == null || !posts.Any())
            {
                Console.WriteLine("No posts to show.");
                return;
            }

            foreach (var post in posts.Take(10))
            {
                Console.WriteLine(post.ToString());
                Console.WriteLine(new string('-', 20));
            }
        }

        static void FollowUser()
        {
            Console.Write("Username to follow: ");
            string target = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(target)) return;

            var targetUser = _users.Find(u => u.Username.Equals(target, StringComparison.OrdinalIgnoreCase));
            if (targetUser == null) throw new SocialException("User does not exist.");

            _currentUser?.Follow(target);
            ConsoleColorWrite(ConsoleColor.Green, $"Now following @{targetUser.Username}");
        }

        static void ListUsers()
        {
            var allUsers = _users.GetAll().OrderBy(u => u).ToList();
            foreach (var u in allUsers)
                Console.WriteLine(u.GetDisplayName());
        }

        static void SaveData()
        {
            try
            {
                File.WriteAllText(_dataFile, "Data saved at " + DateTime.Now);
            }
            catch (Exception ex) { LogError(ex); }
        }

        static void LoadData()
        {
            if (File.Exists(_dataFile))
                Console.WriteLine("Previous session data detected.");
        }

        static void LogError(Exception ex)
        {
            try { File.AppendAllText("error.log", $"[{DateTime.Now}] {ex}"); }
            catch
            {
                
            }
        }

        static void ConsoleColorWrite(ConsoleColor color, string message)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = prev;
        }
    }
}