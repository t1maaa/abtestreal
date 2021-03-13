using System.Threading.Tasks;
using abtestreal.VM;

namespace abtestreal.DAL.Commands
{
    public interface IUpdateUserBulkCommand
    {
        Task<UpdateUserBulkResponse> ExecuteAsync(UpdateUserBulkRequest request);
    }
}