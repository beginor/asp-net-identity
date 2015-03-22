using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetIdentity.WebApi.Data {

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        
        public ApplicationDbContext() : base("DefaultConnection", false) {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.Log = s => Console.WriteLine(s);
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}