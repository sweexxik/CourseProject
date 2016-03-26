using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class UserViewModel
    {
        
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public IEnumerable<Medal> Medals { get; set; }

        public string AvatarUri { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}