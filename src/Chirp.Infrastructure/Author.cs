using Microsoft.AspNetCore.Identity;
namespace Chirp.Infrastructure;


/// <summary>

/// Represents the user accounts in Chirp! 

///</summary>
/// <remarks>
/// Newly created users have identical Username and Email fields
/// </remarks>


public class Author : IdentityUser
{
    //
    //Default values for get-set properties:
    //https://www.tutorialsteacher.com/articles/set-default-value-to-property-in-csharp
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();

    public bool IsDeleted { get; set; }

}
/// <summary>

/// Represents follow-relationships in Chirp!
/// Follows are queried when deciding what cheeps are displayed in the Chirp.Web/Pages/FollowTimeline.cshtml

///</summary>
public class Follow
{
    public string? FollowerId { get; set; }
    public string? FollowingId { get; set; }
    public Author Follower { get; set; } // the people that follows you
    public Author Following { get; set; } // the people you follow


}