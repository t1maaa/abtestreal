using System;
using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.Db.Models;
using abtestreal.VM;

namespace abtestreal.DAL.Commands
{
    public class DeleteUserBulkCommand : IDeleteUserBulkCommand
    {
        private readonly AppDbContext _context;

        public DeleteUserBulkCommand(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<ListResponse<int>> ExecuteAsync(ListRequest<UserRequest> request)
        {
            if (request.Items.Length == 0)
                return new ListResponse<int>()
                {
                    Items = Array.Empty<int>()
                };

            var users = request.Items.Select(u => new User
            {
                Id = u.Id
            }).ToList();

            try
            {
                _context.Users.RemoveRange(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new ListResponse<int>()
            {
                Items = users.Select(u => u.Id).ToArray(),
            };
        }
    }
}