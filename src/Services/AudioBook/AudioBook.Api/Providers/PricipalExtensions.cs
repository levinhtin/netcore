using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel;

namespace AudioBook.API.Providers
{
    public static class PricipalExtensions
    {
        /// <summary>
        /// Get UserId
        /// </summary>
        /// <param name="principal">Principal object</param>
        /// <returns>User Id</returns>
        public static int GetUserId(this IPrincipal principal)
        {
            if (principal == null)
            {
                return -1;
            }

            var claimsPrincipal = principal as ClaimsPrincipal;

            if (claimsPrincipal != null)
            {
                foreach (var identity in claimsPrincipal.Identities)
                {
                    Claim idClaim = identity.FindFirst(JwtClaimTypes.Id);

                    if (idClaim != null)
                    {
                        return int.Parse(idClaim.Value);
                    }
                }
            }

            return -1;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get current Id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return Convert.ToInt16(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        /// <summary>
        /// Get current Username
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }


        /// <summary>
        /// Get current Email
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
