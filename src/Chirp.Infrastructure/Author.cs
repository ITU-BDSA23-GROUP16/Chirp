using Microsoft.AspNetCore.Identity;
namespace Chirp.Infrastructure;
public class Author : IdentityUser
{
    //Name is used for searching and queries, Email is used to log in.
    //Default values for get-set properties:
    //https://www.tutorialsteacher.com/articles/set-default-value-to-property-in-csharp
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();

    public ICollection<Follow> Followers;
    public ICollection<Follow> Followings;
}

public class Follow
{
    public string? FollowerId { get; set; }
    public string? FollowingId { get; set; }
    public Author Follower { get; set; } // the people that follows you
    public Author Following { get; set; } // the people you follow


}