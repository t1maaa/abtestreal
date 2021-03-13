using System.Threading.Tasks;
using abtestreal.VM;

namespace abtestreal.DAL.Queries
{
    public interface IUsersRollingRetention
    {
        Task<UsersRollingRetentionResponse> RunAsync(int days);
    }
}