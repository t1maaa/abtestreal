using System.Threading.Tasks;
using abtestreal.Db.Models;
using abtestreal.VM;

namespace abtestreal.DAL.Commands
{
    public interface ICreateUserBulkCommand
    {
        Task<CreateUserBulkResponse> ExecuteAsync(CreateUserBulkRequest request);
    }
}