using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AspNetIdentity.WebApi.Data;
using AspNetIdentity.WebApi.Models;
using Microsoft.AspNet.Identity;
using AspNetIdentity.WebApi.Models;
using System.Net.Http;
using System.Security.Claims;

namespace AspNetIdentity.WebApi.Controllers {

    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController {

        [Route("users")]
        public IHttpActionResult GetUsers() {
            return Ok(AppUserManager.Users.ToList().Select(u => TheModelFactory.Create(u)));
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id) {
            var user = await AppUserManager.FindByIdAsync(id);

            if (user != null) {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username) {
            var user = await AppUserManager.FindByNameAsync(username);

            if (user != null) {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        public IHttpActionResult Login(LoginModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var user = AppUserManager.FindByName(model.Username);
            if (user == null) {
                return BadRequest(string.Format("User {0} does not exist.", model.Username));
            }
            if (!AppUserManager.CheckPassword(user, model.Password)) {
                return BadRequest("Invalid password.");
            }
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            var auth = Request.GetOwinContext().Authentication;
            auth.SignIn(identity);
            return Ok();
        }

        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserModel createUserModel) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date
            };

            IdentityResult addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded) {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }
    }
}
