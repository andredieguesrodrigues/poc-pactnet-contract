using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReadMe.Provider.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var dataDirectory = Directory.CreateDirectory(Path.Combine("..", "..", "..", "data"));
            var fileData = System.IO.File.ReadAllText(Path.Combine(dataDirectory.FullName, "users.json"));
            var usersData = string.IsNullOrEmpty(fileData)
                ? new List<User>()
                : JsonConvert.DeserializeObject<List<User>>(fileData);
            var requestedUser = usersData.FirstOrDefault(user => string.Equals(user.Id, id, StringComparison.InvariantCultureIgnoreCase));
            if (usersData != default)
            {
                return Ok(requestedUser);
            }
            return BadRequest();
        }
    }
}
