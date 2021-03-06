﻿using System.Net.Http;
using System.Web.Http;
using AspNetIdentity.WebApi.Data;
using AspNetIdentity.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AspNetIdentity.WebApi.Controllers {

    public class BaseApiController : ApiController {

        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager {
            get {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ModelFactory TheModelFactory {
            get {
                if (_modelFactory == null) {
                    _modelFactory = new ModelFactory(Request, AppUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result) {
            if (result == null) {
                return InternalServerError();
            }

            if (!result.Succeeded) {
                if (result.Errors != null) {
                    foreach (string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid) {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}