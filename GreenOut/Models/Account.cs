using Microsoft.AspNetCore.Identity;

namespace GreenOut.Models
{
    public class Account: IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }


        public string Address { get; set; }
    }
}
