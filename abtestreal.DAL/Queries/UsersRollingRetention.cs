using System;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.VM;
using Microsoft.EntityFrameworkCore;

namespace abtestreal.DAL.Queries
{
    public class UsersRollingRetention : IUsersRollingRetention
    {
        private readonly AppDbContext _context;

        public UsersRollingRetention(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsersRollingRetentionResponse> RunAsync(int days)
        {
            var usersLastSeenXdaysOrLater = await _context.Users.CountAsync(u => (u.LastSeen - u.Registered).Days >= days);
          //  if (usersLastSeenXdaysOrLater == 0)
          //  {
          //      return $"Count of returned users in {days} days or later is 0";
          //  }
            var usersAppInstalledXdaysOrEarlier = await _context.Users.CountAsync(u => (DateTime.Now - u.Registered).Days >= days);
            
            if(usersAppInstalledXdaysOrEarlier == 0)
            {
                return new UsersRollingRetentionResponse
                {
                    Days = days,
                    Percent = $"Count of registered users in {days} days ago or earlier is 0"
                };
            }
            
            var rollingRetention = (float) usersLastSeenXdaysOrLater / (float) usersAppInstalledXdaysOrEarlier;

            return new UsersRollingRetentionResponse
            {
                Days = days,
                Percent = rollingRetention.ToString("P")
            };
        }
    }
}