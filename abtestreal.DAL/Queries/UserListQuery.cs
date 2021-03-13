using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.VM;
using Microsoft.EntityFrameworkCore;

namespace abtestreal.DAL.Queries
{
    public class UserListQuery : IUserListQuery
    {
        private readonly AppDbContext _dbContext;
        
        public UserListQuery(AppDbContext context)
        {
            _dbContext = context;
        }
        
        public async Task<ListResponse<UserResponse>> RunAsync()
        {
            var users = await _dbContext.Users.AsNoTracking().Select(u => new UserResponse
            {
                Id = u.Id,
                Registered = u.Registered.Date,
                LastSeen = u.LastSeen.Date,
            }).OrderBy(u => u.Id).ToArrayAsync();

            return new ListResponse<UserResponse>
            {
                Items = users
            };
        }
    }
}