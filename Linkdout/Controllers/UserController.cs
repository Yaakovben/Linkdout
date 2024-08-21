using Linkdout.DTO;
using Linkdout.Models;
using Linkdout.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Linkdout.Controllers
{ //
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JwtService jwtService;

        public UserController(UserService _userService, JwtService _jwtService) 
        { 
            userService = _userService;
            jwtService = _jwtService;
        }

        // ID פונקצייה לקבלת משתשמש לפי 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<SingleUserResponseDTO>> GetUser(int id)
        {
            UserModel user = await userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }



        // פונקצייה להוספת משתמש חדש
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Register([FromBody] UserModel user)
        {
            int userId = await userService.Register(user);
            if (userId != 0)
            {
               
                return Ok(userId);
            }
            else
            {
                return BadRequest();
            }
        }

        // פונקצייה לכניסת משתמש קיים
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> login([FromBody] UserModel user)
        {
            UserModel userFromDb = await userService.GetUserByUserNameAndPassword(user.UserName, user.Password);
            if (userFromDb == null)
            {
                return Unauthorized("המשתמש אינו קיים במערכת");
            }
            string token = jwtService.genJWToken(userFromDb);
            return Ok(token);


        }



    }
}
