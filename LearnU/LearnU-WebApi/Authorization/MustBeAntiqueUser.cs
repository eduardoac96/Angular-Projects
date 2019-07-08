using LearnU_WebApi.Services.User;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnU_WebApi.Authorization
{
    public class MustBeAntiqueUser
    {
        public class MustBeAntiqueUserRequirement : IAuthorizationRequirement
        {
            public int MinimumSeconds { get; }

            // This constructor can be used for storing additional contextual information, 
            // for example we can accept arguments in the constructor to be checked against our requirements
            public MustBeAntiqueUserRequirement(int minimumSeconds)
            {
                MinimumSeconds = minimumSeconds;
            }
        }

        public class MustBeAntiqueUserHandler : AuthorizationHandler<MustBeAntiqueUserRequirement>
        {
            private IUserRepository _repository;

            public MustBeAntiqueUserHandler(IUserRepository repository)
            {
                _repository = repository;
            }

            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeAntiqueUserRequirement requirement)
            {
                var successfulParse = Guid.TryParse(context.User.Identity.Name, out Guid userId);
                if (!successfulParse)
                {
                    context.Fail();
                    return;
                }
                var repoUser = await _repository.GetUser(userId);
                if (repoUser == null || DateTime.Now.Subtract(repoUser.RegistryDate.Value).TotalSeconds <= requirement.MinimumSeconds)
                {
                    context.Fail();
                    return;
                }
                context.Succeed(requirement);
            }
        }
    }
}
