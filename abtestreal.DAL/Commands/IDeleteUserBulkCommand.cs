using System.Threading.Tasks;
using abtestreal.VM;

namespace abtestreal.DAL.Commands
{
    public interface IDeleteUserBulkCommand
    {
        Task<ListResponse<int>> ExecuteAsync(ListRequest<UserRequest> request);
    }
}