using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApppUser> _userManager;
        private readonly ITokenServices _tokenService;
        private readonly SignInManager<ApppUser> _signInManager;
        public AccountController(UserManager<ApppUser> userManager, ITokenServices tokenService, SignInManager<ApppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("registro")]

        public async Task<IActionResult> registerUser([FromBody] RegisterDTO register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var appUser = new ApppUser
                {
                    UserName = register.NombreUsuario,
                    Email = register.Email
                };
                var result = await _userManager.CreateAsync(appUser, register.Password);
                if(result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                NombreUsuario = appUser.UserName,
                                Email = appUser.Email,
                                token = _tokenService.createToken(appUser)
                            }
                        );
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }else
                {
                  return StatusCode(500, result.Errors);  
                }
            }
            catch (Exception ex)
            { 
                
                return StatusCode(404, ex);
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return Conflict(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.NombreUsuario.ToLower());
            if (user == null)
            {
                return Unauthorized("Usuario invalidi");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if(!result.Succeeded)
            {
                return Unauthorized("Error al ingresar la contraseña");
            }
            return Ok(
                new NewUserDTO{
                    NombreUsuario = user.UserName,
                    Email = user.Email,
                    token = _tokenService.createToken(user)
                }
            );

        }
    }
}
