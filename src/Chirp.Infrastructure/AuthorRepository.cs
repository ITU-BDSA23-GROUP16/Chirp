using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;


public class AuthorRepository : IAuthorRepository
{

    private readonly ChirpDBContext _context;

    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;

    }

    public async Task CreateAuthor(AuthorDTO author)
    {

        var newAuthor = new Author
        {
            UserName = author.Name, //Name of AuthorDTO
            Email = author.Email //Email of AuthorDTO
        };
        var existing = await _context.Authors.Where(c => c.UserName == author.Name).FirstOrDefaultAsync();
        if (existing != null)
        {
            throw new ArgumentException("Author already exists in database!", nameof(author));
        }
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
    }


    public async Task<AuthorDTO> FindAuthorByName(string author)
    {

        return await _context.Authors
        .Where(a => a.UserName.Contains(author))
        .OrderByDescending(a => a.UserName)
        .Select(a => new AuthorDTO(a!.UserName, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.UserName, c.Message, c.TimeStamp)))).SingleOrDefaultAsync();
    }


    public async Task<AuthorDTO> FindAuthorByEmail(string email)
    {
        return await _context.Authors
        .Where(a => a.Email != null && a.Email == email)
        .OrderByDescending(a => a.Email)
        .Select(a => new AuthorDTO(a!.UserName, a.Email, a.Cheeps.Select(c => new CheepDTO(c.Author.UserName, c.Message, c.TimeStamp)))).SingleOrDefaultAsync();


    }

    public async Task CreateFollow(AuthorDTO followerDto, AuthorDTO followedDto )
    {
        var follower = await _context.Authors.SingleAsync(c => c.UserName == followerDto.Name);
        var followed = await _context.Authors.SingleAsync(c => c.UserName == followedDto.Name);        

        var newFollow = new Follow
        {
            FollowerId = follower.Id,
            FollowingId = followed.Id,
            Follower = follower,
            Following = followed
        };
        
        _context.Follows.Add(newFollow);
        await _context.SaveChangesAsync();
    }



        //Find who follows specific author
        public async Task<IEnumerable<AuthorDTO>> GetFollowed(string author)
    {   
        return await _context.Follows
        .Where(f => f.Following.UserName == author)
        .Select(a => new AuthorDTO(a.Follower.UserName, a.Follower.Email, a.Follower.Cheeps.Select(c => new CheepDTO(c.Author.UserName, c.Message, c.TimeStamp))))
        .ToListAsync();
    }
        
        //Finds who a specific author follows
        //f.Follower are all the potential people that follow someone
        //f.Following is a group of people that are being followed by someone
        public async Task<IEnumerable<AuthorDTO>> GetFollowing(string author)
    {
        return await _context.Follows
        .Where(f => f.Follower.UserName == author)
        .Select(a => new AuthorDTO(a.Following.UserName, a.Following.Email, a.Following.Cheeps.Select(c => new CheepDTO(c.Author.UserName, c.Message, c.TimeStamp))))
        .ToListAsync();
        
    }

        public async Task RemoveFollow(AuthorDTO followerDto, AuthorDTO followingDto)
    {   
        var follow = await _context.Follows
        .SingleAsync(f => f.Following.UserName == followingDto.Name && f.Follower.UserName == followerDto.Name);
        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();
    }    
}