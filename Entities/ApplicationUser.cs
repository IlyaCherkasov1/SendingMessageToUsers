using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessageToUsers.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string userName, string email) :base(userName) 
        {
            Email = email;
        }
   
        public string GivenName { get; set; }
    }
}
