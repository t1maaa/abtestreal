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
        
        public async Task<DeleteUserBulkResponse> ExecuteAsync(DeleteUserBulkRequest request)
        {
            if (request.Users.Length == 0)
                return new DeleteUserBulkResponse
                {
                    Count = 0,
                    Ids = Array.Empty<int>()
                };

            var users = request.Users.Select(u => new User
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
            
            return new DeleteUserBulkResponse
            {
                Ids = users.Select(u => u.Id).ToArray(),
                Count = users.Count
            };
        }
    }
}