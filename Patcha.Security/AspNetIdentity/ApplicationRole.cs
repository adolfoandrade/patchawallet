using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Patcha.Security
{
    [CollectionName("Role")]
    public class ApplicationRole : MongoIdentityRole<string>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName) 
            : base(roleName)
        {

        }
    }
}
