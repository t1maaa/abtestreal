using System.Threading.Tasks;
using abtestreal.VM;

namespace abtestreal.DAL.Queries
{
    public interface IUsersLifetimeQuery
    {
        Task<ListResponse<UsersLifetimeResponse>> RunAsync();
    }
}