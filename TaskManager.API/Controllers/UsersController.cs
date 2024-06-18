using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;
using TaskManager.API.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public UsersController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Всем привет!");
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userModel)
        {
            if (userModel != null)
            {
                User newUser = new User(userModel.FirstName, userModel.LastName, userModel.Email, userModel.Password,
                                userModel.Status, userModel.Phone, userModel.Photo);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, UserDTO user)
        {
            if (user != null)
            {
                User? userForUpdate = _db.Users.FirstOrDefault(u => u.Id == id);
                if (userForUpdate != null)
                {
                    userForUpdate.FirstName = user.FirstName;
                    userForUpdate.LastName = user.LastName;
                    userForUpdate.Password = user.Password;
                    userForUpdate.Phone = user.Phone;
                    userForUpdate.Photo = user.Photo;
                    userForUpdate.Email = user.Email;
                    userForUpdate.Status = user.Status;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();

                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            User? user = _db.Users.FirstOrDefault(u =>u.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _db.Users.Select(u => u.ToDto()).ToListAsync();
        }

        [HttpPost("create/all")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserDTO> users)
        {
            if (users != null &&  users.Count > 0)
            {
                var newUsers = users.Select(u => new User(u));
                _db.Users.AddRange(newUsers);
                await _db.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }
    }
}
