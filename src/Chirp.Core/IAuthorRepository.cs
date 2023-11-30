namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
//Skal en authorDTO's liste være en ICollection eller IEnumerable, Og hvis det skal være Icollection, skal alt returneres som Icollection i CheepRepository?
//Gør det en forskel for vores one to many relationships?
public interface IAuthorRepository
{
// Task Create(AuthorDTO author);

Task CreateAuthor(AuthorDTO author);
Task CreateFollow(AuthorDTO followerDto, AuthorDTO followingDto);
Task RemoveFollow(AuthorDTO followerDto, AuthorDTO followingDto);

Task <AuthorDTO> FindAuthorByName(string name);

Task <AuthorDTO> FindAuthorByEmail(string email);


Task DeleteAuthor(string author);

Task<IEnumerable<AuthorDTO>> GetFollowed(string author);

Task<IEnumerable<AuthorDTO>> GetFollowing(string author);

Task<Boolean> FollowExists(AuthorDTO followerDto, AuthorDTO followingDto);



}