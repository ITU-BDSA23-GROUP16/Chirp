namespace Chirp.Core;
public record AuthorDTO(string Name, string Email, IEnumerable<CheepDTO> Cheeps);
/// <summary>
/// IAuthorRepository is the interface defining the methods for retrieving data, using data transfer objects. 
/// This interface defines all the relevant methods of an AuthorDTO and its properties.
/// The interface defines methods that can retrieve, delete, and create Authors, as well as forming a follow relation.
/// </summary>
public interface IAuthorRepository
{

Task CreateAuthor(AuthorDTO author);
Task CreateFollow(AuthorDTO followerDto, AuthorDTO followingDto);
Task RemoveFollow(AuthorDTO followerDto, AuthorDTO followingDto);

Task <AuthorDTO> FindAuthorByName(string name);

Task <AuthorDTO> FindAuthorByEmail(string email);


Task DeleteAuthor(string author);

Task<IEnumerable<AuthorDTO>> GetFollowed(string author,int pageSize, int page);
Task<IEnumerable<AuthorDTO>> GetAllFollowed(string author);
Task<IEnumerable<AuthorDTO>> GetFollowing(string author,int pageSize, int page);
Task<IEnumerable<AuthorDTO>> GetAllFollowing(string author);
Task<IEnumerable<AuthorDTO>> GetAllAuthors();
Task<IEnumerable<AuthorDTO>> GetAuthors(int pageSize, int page);
Task<bool> FollowExists(AuthorDTO followerDto, AuthorDTO followingDto);



}