using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Mailjet.Client.Resources;

namespace Shared.Models.Responses
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        // public AuthenticateResponse(User user, string token)
        // {
        //     FirstName = user.FirstName;
        //     LastName = user.LastName;
        //     Email = user.Email;
        //     Token = token;
        // }
    }
}