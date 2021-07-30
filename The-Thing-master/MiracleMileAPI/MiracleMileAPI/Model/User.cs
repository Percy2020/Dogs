using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public bool AgreeMarketing { get; set; }
        public bool SubscribeToEmailNotification { get; set; }
        public Address Address { get; set; }

    }
}
