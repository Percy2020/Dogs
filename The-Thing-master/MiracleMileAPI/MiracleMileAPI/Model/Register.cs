using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Model
{
    public class Register
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AgreeMarketing { get; set; }
        public bool ReceiveNotificationsByMail { get; set; }
    }

}
