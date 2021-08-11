using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Services
{
    public class CheckUserDataService
    {
        public int GetUserTokenId(ClaimsPrincipal claim)
        {
            if (claim.Identity.Name == null) return 0;

            if (Int32.TryParse(claim.Identity.Name, out int id))
            {
                return id;
            }
            else
            {
                return 0;
            }
        }

    }
}
