using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiracleMileAPI.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiracleMileAPI.Filters
{
    public class ValidateToken : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeaders = context.HttpContext.Request.Headers["Authorization"];

            if (authHeaders.Count < 1)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var pattern = new Regex(@"^Bearer\s+(.+)$", RegexOptions.IgnoreCase);
                var match = pattern.Match(authHeaders[0]);

                if (match.Success)
                {
                    try
                    {

                        if (!TokenData.ValidateToken(match.Groups[1].Value))
                        {
                            context.Result = new UnauthorizedResult();
                        }
                        else
                        {
                            var success = true;
                        }

                    }
                    catch
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

    }
}
