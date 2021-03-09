using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using abtestreal.Db;
using abtestreal.Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace abtestreal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // public new record User(int id, string name, string surname);
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var l = new List<User>
            {
                new User
                {
                    Id = 1, Registered = DateTime.Today.Date, LastSeen = DateTime.Now.Date
                },
                new User
                {
                    Id = 2, Registered = DateTime.Today.Date, LastSeen = DateTime.Now.Date
                }
            };
            
            
            
            return Ok(l);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User[] users)
        {
            return Ok(users);
        }
    }
}