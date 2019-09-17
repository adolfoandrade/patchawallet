using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Patcha.Security
{
    public class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            context.Result.ValidatedRequest.ClientClaims.Add(new Claim("session_id", Guid.NewGuid().ToString()));

            return Task.CompletedTask;
        }
    }
}
