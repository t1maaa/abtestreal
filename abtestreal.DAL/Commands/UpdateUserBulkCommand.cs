using System;
using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.Db.Models;
using abtestreal.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

namespace abtestreal.DAL.Commands
{
    public class UpdateUserBulkCommand : IUpdateUserBulkCommand
    {
        private readonly AppDbContext _context;

        public UpdateUserBulkCommand(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<UpdateUserBulkResponse> ExecuteAsync(UpdateUserBulkRequest request)
        {
            if (request.Users.Length == 0)
                return new UpdateUserBulkResponse
                {
                    Count = 0,
                    Ids = Array.Empty<int>()
                };

            var users = request.Users.Select(u => new User
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

            return new UpdateUserBulkResponse
            {
                Count = users.Count,
                Ids = users.Select(u => u.Id).ToArray()
            };
            // var id = new NpgsqlParameter<int>("id",request.Users[0].Id);
            // _context.Users.FromSqlRaw("UPDATE Users")
        }
    }
}