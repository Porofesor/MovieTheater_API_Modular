using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.IdentityCore.JWT.Models
{
    public class VerifyEmail
    {
        public async Task<bool> Handle(Guid token)
        {
            // Implement the email verification logic using the token
            return true;
        }
    }
}
