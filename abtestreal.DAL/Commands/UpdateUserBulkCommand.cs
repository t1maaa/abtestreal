using System;
using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.Db.Models;
using abtestreal.VM;
using Microsoft.EntityFrameworkCore;

namespace abtestreal.DAL.Commands
{
    public class UpdateUserBulkCommand : IUpdateUserBulkCommand
    {
        private readonly AppDbContext _context;

        public UpdateUserBulkCommand(AppDbContext context)
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
                Id = u.Id,
                Registered = u.Registered,
                LastSeen = u.LastSeen
            }).ToList();

            try
            {
                _context.Users.AttachRange(users);
                foreach (var user in users)
                {
                    _context.Entry(user).State = EntityState.Modified;
                }
                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new ListResponse<int>()
            {
                Items = users.Select(u => u.Id).ToArray()
            };
        }
    }
}