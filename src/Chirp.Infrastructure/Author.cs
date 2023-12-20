namespace Chirp.Infrastructure;


/// <summary>

/// Represents the user accounts in Chirp! Inherits from the IdentityUser class in the AspNetCore.Identity package

///</summary>
/// <remarks>
/// Newly created users have identical Username and Email fields
/// </remarks>


public class Author : IdentityUser
{

    //Default values for get-set properties:
    //https://www.tutorialsteacher.com/articles/set-default-value-to-property-in-csharp
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();

    public ICollection<Follow> Followers;
    public ICollection<Follow> Followings;
}
/// <summary>
/// Represents follow-relationships in Chirp!
/// Follows are queried when deciding what cheeps are displayed in the Follow Timeline and the list of followed users.
///</summary>

public class Follow
{
    public string? FollowerId { get; set; }
    public string? FollowingId { get; set; }
    public required Author Follower { get; set; } // the people that follow you
    public required Author Following { get; set; } // the people you follow


}