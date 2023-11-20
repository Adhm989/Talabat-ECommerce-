using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.IdentitySeeding
{
    public class AppIdentityDbSeeding
    {
        public static async Task seedUserAsync (UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any ())
            {
                var user = new AppUser()
                {
                    DisplayName = "Adham Rabie",
                    Email = "adhmrabie2@gmail.com",
                    UserName = "adhamrabie",
                    PhoneNumber = "01128239474"
                };
                await userManager.CreateAsync (user , "P@ssw0rd");
            }
        }
    }
}
