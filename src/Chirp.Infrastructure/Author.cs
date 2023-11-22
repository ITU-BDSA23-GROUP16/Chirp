using Microsoft.AspNetCore.Identity;
namespace Chirp.Infrastructure;
public class Author : IdentityUser
{  
    //Name is used for searching and queries, Email is used to log in.
    //Default values for get-set properties:
    //https://www.tutorialsteacher.com/articles/set-default-value-to-property-in-csharp
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();
    public ICollection<Author> Follower;
    public ICollection<Author> Followed;


}

public class Follow
{
    public string FollowerId;
    public string FollowedId;
}