using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainLayer.DTOs;

namespace WebApiColegios.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AccountsController"/>.
        /// </summary>
        /// <param name="userManager">Administrador de usuarios.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <param name="signInManager">Administrador de inicio de sesión.</param>
        public AccountsController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="userCredentials">Credenciales del usuario.</param>
        /// <returns>Respuesta de autenticación.</returns>
        [HttpPost("register", Name = "registerUser")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Register(UserCredentialsDTO userCredentials)
        {
            var user = new IdentityUser
            {
                UserName = userCredentials.Email,
                Email = userCredentials.Email
            };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// Inicia sesión de un usuario.
        /// </summary>
        /// <param name="userCredentials">Credenciales del usuario.</param>
        /// <returns>Respuesta de autenticación.</returns>
        [HttpPost("login", Name = "LoginUser")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login(UserCredentialsDTO userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Incorrect login");
            }
        }

        /// <summary>
        /// Renueva el token de autenticación.
        /// </summary>
        /// <returns>Respuesta de autenticación.</returns>
        [HttpGet("RenewToken", Name = "renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthenticationResponseDTO>> Renew()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var userCredentials = new UserCredentialsDTO()
            {
                Email = email
            };

            return await BuildToken(userCredentials);
        }

        /// <summary>
        /// Construye el token de autenticación.
        /// </summary>
        /// <param name="userCredentials">Credenciales del usuario.</param>
        /// <returns>Respuesta de autenticación.</returns>
        private async Task<AuthenticationResponseDTO> BuildToken(UserCredentialsDTO userCredentials)
        {
            var claims = new List<Claim>()
        {
            new Claim("email", userCredentials.Email),
        };

            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new AuthenticationResponseDTO()
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };
        }

        /// <summary>
        /// Asigna el rol de administrador a un usuario.
        /// </summary>
        /// <param name="editAdminDTO">Datos del usuario.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("MakeAdmin", Name = "makeAdmin")]
        public async Task<ActionResult> MakeAdmin(EditAdminDTO editAdminDTO)
        {
            var user = await userManager.FindByEmailAsync(editAdminDTO.Email);
            await userManager.AddClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }

        /// <summary>
        /// Elimina el rol de administrador de un usuario.
        /// </summary>
        /// <param name="editAdminDTO">Datos del usuario.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("RemoveAdmin", Name = "removeAdmin")]
        public async Task<ActionResult> RemoveAdmin(EditAdminDTO editAdminDTO)
        {
            var user = await userManager.FindByEmailAsync(editAdminDTO.Email);
            await userManager.RemoveClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }
    }

}
