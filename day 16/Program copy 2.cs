using System;
namespace MiniSocialMedia{
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

    public partial class User
    {
        
    }

    public class Post
    {
        public User Author{get;init;}
        public string Content{get;init;}

    }

class Program
{
    static void Main()
    {
        
    }
}
}


