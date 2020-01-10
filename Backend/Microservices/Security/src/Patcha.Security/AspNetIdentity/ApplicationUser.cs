using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Patcha.Security
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<string>
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string userName, string email) 
            : base(userName, email)
        {

        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
