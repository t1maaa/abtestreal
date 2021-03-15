using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.VM;

namespace abtestreal.DAL.Queries
{
    public class UsersLifetimeQuery : IUsersLifetimeQuery
    {
        private readonly AppDbContext _context;

        public UsersLifetimeQuery(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<ListResponse<UsersLifetimeResponse>> RunAsync()
        {
            var days = _context.Users
                .Select(u => (u.LastSeen - u.Registered).Days).AsEnumerable().GroupBy(s => s)
                .ToDictionary(s => s.Key, days => days.Count());
            var result = days.Select(d => new UsersLifetimeResponse
            {
                Days = d.Key,
                Count = d.Value
            }).OrderBy(o => o.Days).ToArray();

            return new ListResponse<UsersLifetimeResponse>
            {
                Items = result
            };
        }
    }
}