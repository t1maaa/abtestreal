using System.Threading.Tasks;
using abtestreal.DAL.Commands;
using abtestreal.DAL.Queries;
using abtestreal.VM;
using Microsoft.AspNetCore.Mvc;

namespace abtestreal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices]IUserListQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await query.RunAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ListRequest<UserRequest> request, [FromServices]ICreateUserBulkCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var response = await command.ExecuteAsync(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ListRequest<UserRequest> request, [FromServices]IUpdateUserBulkCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var response = await command.ExecuteAsync(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]ListRequest<UserRequest> request, [FromServices]IDeleteUserBulkCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var response = await command.ExecuteAsync(request);
            return Ok(response);
        }

        
        [HttpGet("lifetime/summary")]
        [Produces("application/json")]
        public async Task<IActionResult> LifetimeSummary([FromServices]IUsersLifetimeQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(await query.RunAsync());
        }

        [HttpGet("rollingretention/{days}")]
        [Produces("application/json")]
        public async Task<IActionResult> RollingRetention([FromRoute] int days,
            [FromServices] IUsersRollingRetention query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (days <= 0)
            {
                return BadRequest("Num of \"Days\" must be positive number");
            }
            
            return Ok(await query.RunAsync(days));
        }
    }
}