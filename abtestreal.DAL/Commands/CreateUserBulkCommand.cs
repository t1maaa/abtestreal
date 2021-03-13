using System;
using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.Db.Models;
using abtestreal.VM;

namespace abtestreal.DAL.Commands
{
    public class CreateUserBulkCommand : ICreateUserBulkCommand
    {
        private readonly AppDbContext _dbContext;

        public CreateUserBulkCommand(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<CreateUserBulkResponse> ExecuteAsync(CreateUserBulkRequest request)
        {
            if (request.Users.Length == 0)
                return new CreateUserBulkResponse
                {
                    Count = 0,
                    Ids = System.Array.Empty<int>()
                };
            var users = request.Users.Select(user => new User {Id = user.Id, Registered = user.Registered, LastSeen = user.LastSeen}).ToList();

            try
            {
                await _dbContext.Users.AddRangeAsync(users);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new CreateUserBulkResponse
           {
               Count = users.Count,
               Ids = users.Select(u => u.Id).ToArray()
           };
        }
    }
}