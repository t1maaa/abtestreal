using System.Threading.Tasks;
using abtestreal.VM;

namespace abtestreal.DAL.Queries
{
    public interface IUserListQuery
    {
        Task<ListResponse<UserResponse>> RunAsync();
    }
}