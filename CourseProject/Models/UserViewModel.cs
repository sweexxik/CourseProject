using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public IEnumerable<MedalViewModel> Medals { get; set; }

        public string AvatarUri { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool IsAdmin { get; set; }
    }
}