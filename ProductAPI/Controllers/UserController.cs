using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductAPI.Models.DTO;
using ProductAPI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly APITranslator translator;

        public UserController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IHttpClientFactory httpClientFactory,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            translator = new APITranslator(httpClientFactory, configuration);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };

            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                await userManager.AddToRoleAsync(user, "Customer");

                return NoContent();
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("Response", await translator.Translate(item.Description));
            }

            return StatusCode(400, ModelState);
        }
    }
}
