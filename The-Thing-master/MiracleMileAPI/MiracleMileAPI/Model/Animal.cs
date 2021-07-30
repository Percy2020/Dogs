using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Model
{
    public class Animal
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
         public string Certificate { get; set; }
         public int OwnerId { get; set; }
        public string OwnerSocialSecurityNumber { get; set; }

    }
}
